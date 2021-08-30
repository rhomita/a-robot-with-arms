using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Robot : MonoBehaviour
{
    [SerializeField] private Rigidbody _body;
    [SerializeField] private List<RobotArm> _arms = new List<RobotArm>();

    private int _armSelected;
    private bool _armsEnabled = false;
    private Vector2 _input;
    private float _heightInput;
    private Animator _animator;

    private float _cooldownToResetArms = CooldownToResetarms;

    private const float Speed = 1000f;
    private const float TurnSpeed = 3f;
    private const float CooldownToResetarms = 2f;

    void Awake()
    {
        _animator = transform.GetComponent<Animator>();
    }

    void Start()
    {
        _armSelected = 1;
        GameManager.Instance.CameraMovement.SetTarget(_body.transform);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ResetArms();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            _armsEnabled = !_armsEnabled;
            if (_armsEnabled)
            {
                _body.velocity = Vector3.zero;
                ChangeSelected();
            }
            else
            {
                _cooldownToResetArms = CooldownToResetarms;
                GameManager.Instance.CameraMovement.SetTarget(_body.transform);
            }
        }

        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _heightInput = Input.GetAxisRaw("Height");
        if (_armsEnabled)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _arms[_armSelected].Extend();
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                _arms[_armSelected].Reduce();
            }
        }
    }

    private void FixedUpdate()
    {
        if (_armsEnabled)
        {
            _animator.SetBool("IsMoving", false);
            _arms[_armSelected].Move(new Vector3(_input.x, _heightInput, _input.y));
        }
        else
        {
            bool isMoving = _input.magnitude > 0.1f;
            _animator.SetBool("IsMoving", isMoving);

            if (isMoving)
            {
                if (_cooldownToResetArms >= 0)
                {
                    _cooldownToResetArms -= Time.deltaTime;
                    if (_cooldownToResetArms <= 0)
                    {
                        ResetArms();
                    }
                }

                _body.velocity = _body.transform.forward * (Time.deltaTime * Speed);

                Quaternion rotation = Quaternion.LookRotation(new Vector3(_input.x, 0, _input.y));
                rotation = Quaternion.Slerp(_body.rotation, rotation, Time.deltaTime * TurnSpeed);
                _body.MoveRotation(rotation);
            }
            else
            {
                _body.velocity = Vector3.zero;
            }
        }
    }

    void ChangeSelected()
    {
        _armSelected++;
        _armSelected = _armSelected % _arms.Count;
        GameManager.Instance.CameraMovement.SetTarget(_arms[_armSelected].Hand);
    }

    private void ResetArms()
    {
        foreach (RobotArm arm in _arms)
        {
            arm.Reset();
        }
    }
}