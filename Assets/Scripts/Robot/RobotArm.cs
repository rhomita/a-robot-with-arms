using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using UnityEngine;

public class RobotArm : MonoBehaviour
{
    [SerializeField] private RobotArmPart _armPartPrefab;
    [SerializeField] private GameObject _hand;
    [SerializeField] private List<RobotArmPart> _armParts = new List<RobotArmPart>();
    [SerializeField] private SoundEffect _extendSoundEffect;
    
    private ConfigurableJoint _handJoint;
    private Rigidbody _handRigidbody;

    private int initArmsQuantity = 0;

    private float _extendCooldown = 0f;
    private float _reduceCooldown = 0f;
    private float _extendPitch;

    private const float ResizeCooldown = .1f;
    private const float MoveSpeed = 120f;
    private const float MaxExtendPitch = 1.5f;
    private const float MinExtendPitch = .5f;
    private const float ExtendPitchIncrease = .01f;


    public Transform Hand => _hand.transform;

    void Awake()
    {
        initArmsQuantity = _armParts.Count;
        _extendPitch = MinExtendPitch;
        
        _extendSoundEffect = transform.GetComponent<SoundEffect>();
        _handJoint = _hand.GetComponent<ConfigurableJoint>();
        _handRigidbody = _hand.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_extendCooldown > 0)
        {
            _extendCooldown -= Time.deltaTime;
        }
        
        if (_reduceCooldown > 0)
        {
            _reduceCooldown -= Time.deltaTime;
        }
    }

    public void Move(Vector3 direction)
    {
        _handRigidbody.AddForce(direction * (MoveSpeed * Time.deltaTime), ForceMode.VelocityChange);
    }

    public void Extend()
    {
        if (_extendCooldown > 0) return;
        _extendCooldown = ResizeCooldown;

        _extendPitch += ExtendPitchIncrease;
        if (_extendPitch >= MaxExtendPitch)
        {
            _extendPitch = MinExtendPitch;
        }
        _extendSoundEffect.Play(_extendPitch);

        Vector3 position = _armParts.Last().transform.position + _armParts.Last().transform.forward;
        Quaternion rotation = _armParts.Last().transform.rotation;

        RobotArmPart robotArmPart = Instantiate(_armPartPrefab, position, rotation);
        RobotArmPart lastArmPart = _armParts.Last();
        robotArmPart.ConnectTo(lastArmPart);

        _armParts.Add(robotArmPart);
        robotArmPart.name = $"Hand-Part-{_armParts.Count}";
        robotArmPart.transform.parent = transform;

        ConnectHandTo(robotArmPart);
    }

    public void Reduce()
    {
        if (_armParts.Count <= initArmsQuantity) return;

        if (_reduceCooldown > 0) return;
        _reduceCooldown = ResizeCooldown;
        
        _extendPitch -= ExtendPitchIncrease;
        if (_extendPitch <= MinExtendPitch)
        {
            _extendPitch = MaxExtendPitch;
        }
        _extendSoundEffect.Play(_extendPitch);
        
        RemoveLastPart();
    }

    public void Reset()
    {
        if (_armParts.Count <= initArmsQuantity)
        {
            _extendCooldown = 0f;
            _reduceCooldown = 0f;
            return;
        }
        
        _extendCooldown = float.MaxValue;
        _reduceCooldown = float.MaxValue;
        RemoveLastPart();
        Reset();
    }

    private void RemoveLastPart()
    {
        RobotArmPart lastRobotArmPart = _armParts.Last();
        _armParts.Remove(lastRobotArmPart);
        Destroy(lastRobotArmPart.gameObject);

        lastRobotArmPart = _armParts.Last();
        ConnectHandTo(lastRobotArmPart);
    }
    
    private void ConnectHandTo(RobotArmPart robotArmPart)
    {
        _handJoint.connectedBody = null;
        _handRigidbody.velocity = Vector3.zero;
        _handRigidbody.angularVelocity = Vector3.zero;
        _handJoint.secondaryAxis = _handJoint.secondaryAxis;
        _handJoint.transform.position = robotArmPart.transform.position + robotArmPart.transform.forward;
        _handJoint.transform.rotation = robotArmPart.transform.rotation;
        _handJoint.connectedBody = robotArmPart.RigidBody;
        
    }
}