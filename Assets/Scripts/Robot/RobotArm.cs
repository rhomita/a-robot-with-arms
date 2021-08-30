using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RobotArm : MonoBehaviour
{
    [SerializeField] private RobotArmPart _armPartPrefab;
    [SerializeField] private ConfigurableJoint _handJoint;
    [SerializeField] private Rigidbody _handRigidbody;
    [SerializeField] private List<RobotArmPart> _armParts = new List<RobotArmPart>();

    private int initArmsQuantity = 0;

    private float _extendCooldown = 0f;
    private float _reduceCooldown = 0f;

    private const float ResizeCooldown = .05f;
    private const float BaseRotationForce = 120f;

    public Transform Hand => _handJoint.transform;

    void Awake()
    {
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

    public void Rotate(Vector3 direction)
    {
        _handRigidbody.AddRelativeForce(direction * (BaseRotationForce * Time.fixedDeltaTime), ForceMode.VelocityChange);
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
        _handJoint.connectedBody = null;
        _handRigidbody.velocity = Vector3.zero;
        _handRigidbody.angularVelocity = Vector3.zero;
        _handJoint.secondaryAxis = _handJoint.secondaryAxis;
        _handJoint.transform.position = robotArmPart.transform.position + robotArmPart.transform.forward;
        _handJoint.transform.rotation = robotArmPart.transform.rotation;
        _handJoint.connectedBody = robotArmPart.RigidBody;
        
    }
}