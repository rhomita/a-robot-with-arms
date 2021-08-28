using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    [SerializeField] private List<RobotArm> _arms = new List<RobotArm>();
    private int selectedArm = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            _arms[selectedArm].Reset();
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            _arms[selectedArm].Extend();
        }

        if (Input.GetKey(KeyCode.S))
        {
            _arms[selectedArm].Reduce();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeSelected();
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            _arms[selectedArm].MoveForward();
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            _arms[selectedArm].Rotate(Vector3.right);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            _arms[selectedArm].Rotate(-Vector3.right);   
        }
        
    }

    void ChangeSelected()
    {
        selectedArm++;
        selectedArm = selectedArm % _arms.Count;
        GameManager.Instance.CameraMovement.SetTarget(_arms[selectedArm].Hand);
    }
}
