using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public Slider oxygenSlider;
    public TMP_Text timerText;
    public float time = 600f;
    private float timeLeft;
    private float timerSpeed = 1;


    private bool stopTimer;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft = time - Time.timeSinceLevelLoad * timerSpeed;

        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60f);

        string textTime = string.Format("{00:00}:{01:00}", minutes, seconds);

        if (timeLeft <= 0)
        {
            stopTimer = true;
        }

        if (stopTimer == false)
        {
            timerText.text = textTime;
            oxygenSlider.value = timeLeft;
        }
    }

    public void ResetTimer()
    {
        stopTimer = false;
        oxygenSlider.maxValue = time;
        oxygenSlider.value = time;
        
        Debug.Log("Resetted time");
    }

    // ----  Controll timer speed ----
    public void resetTimerSpeed()
    {
        timerSpeed = 1;
    }

    public void setTimerSpeed(float value)
    {
        if (value >= 0)
        {
            timerSpeed = value;
        }
    }
}
