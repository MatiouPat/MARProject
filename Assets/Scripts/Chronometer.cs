using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chronometer : MonoBehaviour
{

    public GameSettings settings;

    public Text chronoText;
    
    private DateTime _beginTime;
    
    private int _minute;

    private int _second;
    
    void Start()
    {
        settings.isBegin = false;
        settings.isFinish = false;
        _minute = 0;
        _second = 0;
    }

    void Update()
    {
        if (!settings.isFinish)
        {
            if (!settings.isBegin)
            {
                _beginTime = DateTime.Now;
            }
        
            DateTime dateNow = DateTime.Now;
            TimeSpan diff = dateNow - _beginTime;
            _minute = diff.Minutes;
            _second = diff.Seconds;
            chronoText.text = _minute + "min " + _second + "s";
        }
    }
}
