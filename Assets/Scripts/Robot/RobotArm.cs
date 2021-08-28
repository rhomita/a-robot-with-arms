using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RobotArm : MonoBehaviour
{
    [SerializeField] private RobotArmPart _armPartPrefab;
    [SerializeField] private Rigidbody _handRigidbody;
    [SerializeField] private List<RobotArmPart> _armParts = new List<RobotArmPart>();

    private ConfigurableJoint _handConfigurableJoint;

    private int initArmsQuantity = 0;

    private float _extendCooldown = 0f;
    private float _reduceCooldown = 0f;

    private const float ResizeCooldown = .1f;
    private const float BaseForwardForce = 10f;
    private const float BaseRotationForce = 100f;

    public Transform Hand => _handRigidbody.transform;

    void Awake()
    {
        _handConfigurableJoint = _handRigidbody.GetComponent<ConfigurableJoint>();
        initArmsQuantity = _armParts.Count;
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

    public void MoveForward()
    {
        _handRigidbody.AddForce(
            Hand.forward * (BaseForwardForce * _armParts.Count * Time.deltaTime),
            ForceMode.VelocityChange);
    }

    public void Rotate(Vector3 direction)
    {
        _handRigidbody.AddRelativeForce(direction * (BaseRotationForce * Time.deltaTime), ForceMode.VelocityChange);
    }

    public void Extend()
    {
        if (_extendCooldown > 0) return;
        _extendCooldown = ResizeCooldown;

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
        _handConfigurableJoint.connectedBody = null;
        _handRigidbody.velocity = Vector3.zero;
        _handRigidbody.angularVelocity = Vector3.zero;
        _handRigidbody.transform.position = robotArmPart.transform.position + robotArmPart.transform.forward;
        _handRigidbody.transform.rotation = robotArmPart.transform.rotation;
        _handConfigurableJoint.connectedBody = robotArmPart.RigidBody;
    }
}