using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    public static TimerController instance;

    public TextMeshProUGUI timeCounterText;

    
    private TimeSpan timePlaying; // used to format the time
    private bool isGoing;
    private float elapsedTime;

    private void Awake()
    {
        if (instance == null)    //checking if instance is null or not becuase we only need one instance of this class
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        timeCounterText.text = "00:00"; //initial time
        isGoing = false;

        BeginTimer();
    }

    private void Update()
    {
        UpdateTimer();
    }


    public void BeginTimer()
    {
        isGoing = true;
        elapsedTime = 0f;
    }

    public void PauseTimer()
    {
        isGoing = false;
    }

    public void ResetTimer()
    {
        isGoing = false;
        timeCounterText.text = "00:00";
        elapsedTime = 0f;
    }

    private void UpdateTimer()
    {
        if (isGoing)
        {
            elapsedTime += Time.deltaTime; //add time between two frames
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss");
            timeCounterText.text = timePlayingStr;
        }
    }
}
