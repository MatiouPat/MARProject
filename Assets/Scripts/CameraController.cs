using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    [Range(0.01f, 1.0f)]
    public float offsetSpeed;

    public float rotationSpeed;

    private Vector3 _cameraOffset;

    public void Start()
    {
        _cameraOffset = transform.position - player.position;
    }

    public void LateUpdate()
    {

        Quaternion cameraAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);

        _cameraOffset = cameraAngle * _cameraOffset;
        
        /*Position*/
        Vector3 newPosition = player.position + _cameraOffset;
        
        Vector3 linearPosition = Vector3.Slerp(transform.position, newPosition, offsetSpeed * Time.deltaTime);
        
        transform.position = linearPosition;
        /**/
        
        transform.LookAt(player);
    }
}
