using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class SkierAi : MonoBehaviour
{
    public float rotationSpeed;
    
    public float verticalSpeed;

    /**
    * <summary>The distance at which the skier begins to see an obstacle or another skier</summary>
    */
    public float distanceOfView;
    
    /**
    * <summary>The distance between the target and the skier that allows the skier to stop moving</summary>
    */
    public float distanceOfTargetValidation;
    
    private Rigidbody _rigidbody;
    
    /**
    * <summary>The skier's position</summary>
    */
    private Vector3 _position;
    
    /**
    * <summary>The position of the target</summary>
    */
    private Vector3 _desiredDirection;
    
    /**
    * <summary>The rotation to which to go</summary>
    */
    private Quaternion _lookRotation;
    
    /**
    * <summary>The direction to which to go</summary>
    */
    private Vector3[] _directions;
    
    /**
    * <summary>The chosen target</summary>
    */
    public Transform _target;
    
    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        _directions = MakeRayDirections();
    }
    
    public void FixedUpdate()
    {
        _desiredDirection = (_target.position - _position).normalized;
        
        _position = transform.position;
        
        
        for (int i = 0; i < _directions.Length; i++) {
            Vector3 dir = transform.TransformDirection (_directions[i]);
            Ray ray = new Ray (_position, dir);
            if (Physics.Raycast(ray, distanceOfView, LayerMask.GetMask("Obstacle")))
            {
                _desiredDirection = -dir.normalized;
            }
        }
        
        _lookRotation = Quaternion.LookRotation(_desiredDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
        
        if (Vector3.Distance(_target.position, _position) > distanceOfTargetValidation)
        {
            _rigidbody.AddForce(transform.forward * verticalSpeed);
        }
    }

    /**
     * <summary>Create a table containing the different directions of the rays used for obstacle detection</summary>
     */
    private Vector3[] MakeRayDirections()
    {
        const int numViewDirections = 90;
        Vector3[] directions = new Vector3[numViewDirections];


        for (int i = 0; i < numViewDirections; i++) {
            float angleIncrement = Mathf.PI * i / numViewDirections;
            
            float x = Mathf.Cos (angleIncrement);
            float y = 0;
            float z = Mathf.Sin (angleIncrement);
            
            directions[i] = new Vector3 (x, y, z);
        }
        return directions;
    }
}