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
    public float timeLeft;

    private bool stopTimer;

    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        oxygenSlider.maxValue = timeLeft;
        oxygenSlider.value = timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        float time = timeLeft - Time.time;

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);

        string textTime = string.Format("{00:00}:{01:00}", minutes, seconds);

        if (time <= 0)
        {
            stopTimer = true;
        }

        if (stopTimer == false)
        {
            timerText.text = textTime;
            oxygenSlider.value = time;
        }
    }
}
