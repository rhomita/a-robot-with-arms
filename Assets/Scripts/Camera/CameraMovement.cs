using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    private Transform _target;

    private const float MoveSpeed = 10f;

    void Start()
    {
        _target = GameManager.Instance.Player;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, _target.position + _offset, Time.deltaTime * MoveSpeed);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}