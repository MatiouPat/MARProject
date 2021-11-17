using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    
    public Vector3 offsetCamera;

    public float offsetSpeed;

    void LateUpdate()
    {
        Vector3 cameraPosition = player.position + offsetCamera;
        
        Vector3 linearPosition = Vector3.Lerp(transform.position, cameraPosition, offsetSpeed * Time.deltaTime);
        
        transform.position = linearPosition;
        
        transform.LookAt(player);
    }
}
