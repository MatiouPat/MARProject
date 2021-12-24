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
    /**
    * <summary>The different targets located at the bottom of the track</summary>
    */
    public Transform[] targets;
    
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
    private Transform _target;
    
    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        _directions = MakeRayDirections();
        _target = targets[0];
    }

    public void FixedUpdate()
    {
        _position = transform.position;

        for (int i = 0; i < targets.Length; i++)
        {
            if (Vector3.Distance(_target.position, _position) > Vector3.Distance(targets[i].position, _position))
            {
                _target = targets[i];
            }
        }
        
        _desiredDirection = (_target.position - _position).normalized;
        _lookRotation = Quaternion.LookRotation(_desiredDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
        
        for (int i = 0; i < _directions.Length; i++) {
            Debug.DrawRay(_position, _directions[i] * distanceOfView, Color.red);
            Vector3 dir = transform.TransformDirection (_directions[i]);
            Ray ray = new Ray (_position, dir);
            if (Physics.SphereCast (ray, 0.27f, distanceOfView, LayerMask.GetMask("Obstacle")))
            {
                Vector3 avoidDirection = -dir.normalized;
                _lookRotation = Quaternion.LookRotation(avoidDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
        
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
        const int numViewDirections = 300;
        Vector3[] directions = new Vector3[300];
        
        float goldenRatio = (1 + Mathf.Sqrt (5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < numViewDirections; i++) {
            float t = (float) i / numViewDirections;
            float inclination = Mathf.Acos (1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin (inclination) * Mathf.Cos (azimuth);
            float y = Mathf.Sin (inclination) * Mathf.Sin (azimuth);
            float z = Mathf.Cos (inclination);
            directions[i] = new Vector3 (x, y, z);
        }
        return directions;
    }
}