using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalController : MonoBehaviour
{
    public GameSettings settings;

    private void OnTriggerEnter(Collider other)
    {
        settings.isFinish = true;
    }
}
