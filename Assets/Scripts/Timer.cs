using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    bool active = false;
    public Text timeText;
    public float seconds = 0;
    public float minutes = 0;

    void Start() {
        
    }

    void Update() {
        if (active) {
            seconds = seconds + Time.deltaTime;
        }
        if (seconds > 59.99f) {
            minutes++;
            seconds = 0;
        }
        int intSeconds = (int) seconds;
        int intMinutes = (int) minutes;
        timeText.text = intMinutes.ToString().PadLeft(2, '0') + ":" + intSeconds.ToString().PadLeft(2, '0');
    }

    public void StartTimer() {
        this.active = true;
    }
    public void StopTimer() {
        this.active = false;
    }

    public void ResetTimer() {
        seconds = 0;
        minutes = 0;
    }

    
}
