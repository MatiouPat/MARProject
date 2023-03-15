using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkierMovement : MonoBehaviour
{

    public float thrust = 10f;
    
    public float rotationSpeed = 3f;
    
    private Rigidbody _rigidbody;
    
    private BoxCollider _boxCollider;
    
    private Vector3 _movementForce;
    
    private Vector3 _rotateForce;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        float vertical = Input.GetAxis("Vertical");
        _movementForce = vertical * transform.forward;

        float horizontal = Input.GetAxis("Horizontal");
        _rotateForce = horizontal * rotationSpeed * Vector3.up;
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        if (true)
        {
            _rigidbody.AddForce(_movementForce * thrust);
        }
    }

    private void Rotate()
    {
        if (true)
        {
            transform.Rotate(_rotateForce);
        }
    }
}
