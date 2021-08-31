using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMenu : MonoBehaviour
{
    [SerializeField] private List<RobotArm> _arms = new List<RobotArm>();
    
    void Start()
    {
        InvokeRepeating("Extend", 0, 1);
    }

    void Extend()
    {
        foreach (RobotArm robotArm in _arms)
        {
            robotArm.Extend();
        }
    }
}
