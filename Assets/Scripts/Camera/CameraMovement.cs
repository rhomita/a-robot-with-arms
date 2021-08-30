using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 _offset = new Vector3(0, 30, -20);
    private Transform _target;

    private const float MoveSpeed = 3f;

    void Awake()
    {
        GameManager.Instance.SetCamera(this);
        transform.rotation = Quaternion.Euler(55, 0, 0);
    }
    
    void LateUpdate()
    {
        if (_target == null) return;
        transform.position = Vector3.Slerp(transform.position, _target.position + _offset, Time.deltaTime * MoveSpeed);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetTemporaryTarget(Transform target, float seconds)
    {
        StartCoroutine(SetTemporaryTargetCoroutine(target, seconds));
    }

    private IEnumerator SetTemporaryTargetCoroutine(Transform target, float seconds)
    {
        Transform previousTarget = _target;
        SetTarget(target);
        yield return new WaitForSeconds(seconds);
        SetTarget(previousTarget);
    }
}