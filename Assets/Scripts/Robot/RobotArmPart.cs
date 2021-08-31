using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArmPart : MonoBehaviour
{
    private RobotArm _robotArm;
    private ConfigurableJoint _configurableJoint;
    private Rigidbody _rigidbody;

    public Rigidbody RigidBody => _rigidbody;
    
    void Awake()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();
        _configurableJoint = transform.GetComponent<ConfigurableJoint>();
    }

    void Start()
    {
        _robotArm = transform.parent.GetComponent<RobotArm>();
    }

    public void ConnectTo(RobotArmPart robotArmPart)
    {
        _configurableJoint.connectedBody = robotArmPart.RigidBody;
    }

    public void Reset()
    {
        _robotArm.Reset(this);
    }
}
