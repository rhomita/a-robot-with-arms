using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArmPart : MonoBehaviour
{
    private ConfigurableJoint _configurableJoint;
    private Rigidbody _rigidbody;

    public Rigidbody RigidBody => _rigidbody;
    
    void Awake()
    {
        _rigidbody = transform.GetComponent<Rigidbody>();
        _configurableJoint = transform.GetComponent<ConfigurableJoint>();
    }

    public void ConnectTo(RobotArmPart robotArmPart)
    {
        _configurableJoint.connectedBody = robotArmPart.RigidBody;
    }
}
