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
    private float sceneTimer;
    private bool hasStarted = false;

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

        
    }

    private void Update()
    {
        BeginTimer(); //start time only if the user started cube movement
        UpdateTimer(); //update timer only if isGoing = true
    }


    public void BeginTimer()
    {
        if (!hasStarted && !PauseMenuController.IsPauseMenuOpened() &&
            (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.Space)))
        {
            isGoing = true;
            elapsedTime = 0f;
            hasStarted = true;
        }
    }

    public void PauseTimer()
    {
        isGoing = false;
    }
    public void PlayTimer()
    {
        isGoing = true;
    }

    public void ResetTimer()
    {
        isGoing = false;
        timeCounterText.text = "00:00";
        elapsedTime = 0f;
    }

    public void SetFreezeColor()
    {
        timeCounterText.color = new Color(0, 0.75f, 0.84f, 1);
    }

    public void SetNormalColor()
    {
        timeCounterText.color = new Color(1, 1, 1, 1);
    }

    public float GetTimeInFloat()
    {
        return elapsedTime;
    }
    public float GetSceneTimer()
    {
        return sceneTimer;
    }
    public string GetTimeInString()
    {
        return timePlaying.ToString("mm':'ss");
    }

    private void UpdateTimer()
    {
        if (isGoing)
        {
            sceneTimer += Time.deltaTime;
            if (!AbilityController.instance.IsFreezeActive())
            {
                elapsedTime += Time.deltaTime; //add time between two frames
                timePlaying = TimeSpan.FromSeconds(elapsedTime);
                string timePlayingStr = timePlaying.ToString("mm':'ss");
                timeCounterText.text = timePlayingStr;
            }
        }
    }

}
