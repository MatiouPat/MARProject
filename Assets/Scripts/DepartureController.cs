using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartureController : MonoBehaviour
{
    public GameSettings settings;

    private void OnTriggerExit(Collider other)
    {
        settings.isBegin = true;
    }
}
