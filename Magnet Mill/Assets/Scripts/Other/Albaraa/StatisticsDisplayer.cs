using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatisticsDisplayer : MonoBehaviour
{
    public TextMeshProUGUI levelsFinishedValue;
    public TextMeshProUGUI cubeRolledValue;
    public TextMeshProUGUI gravitySwitValue;
    public TextMeshProUGUI checkpointActValue;
    public TextMeshProUGUI abilitiesValue;
    public TextMeshProUGUI deathsValue;
    public TextMeshProUGUI totalScoreValue;
    public TextMeshProUGUI timePlayedValue;
    public TextMeshProUGUI level1HighScoreValue;
    public TextMeshProUGUI level2HighScoreValue;
    public TextMeshProUGUI level3HighScoreValue;
    public TextMeshProUGUI level4HighScoreValue;

    void Start()
    {
        SetValue(UserData.numOfLevelsFinished, levelsFinishedValue);
        SetValue(UserData.numOfGravitySwitched, gravitySwitValue);
        SetValue(UserData.numOfCheckpointsActivated, checkpointActValue);
        SetValue(UserData.numOfAbilitiesUsed, abilitiesValue);
        SetValue(UserData.numOfDeaths, deathsValue);
        SetValue(UserData.level1HighScore, level1HighScoreValue);
        SetValue(UserData.level2HighScore, level2HighScoreValue);
        SetValue(UserData.level3HighScore, level3HighScoreValue);
        SetValue(UserData.level4HighScore, level4HighScoreValue);

        if(UserData.GetInt(UserData.numOfCubeRolled) < 1000000)
        {
            cubeRolledValue.text = UserData.GetInt(UserData.numOfCubeRolled).ToString();
        }
        else
        {
            cubeRolledValue.text = "999999+";
        }

        if (UserData.GetInt(UserData.numOfTotalScore) < 1000000)
        {
            totalScoreValue.text = UserData.GetInt(UserData.numOfTotalScore).ToString();
        }
        else
        {
            totalScoreValue.text = "999999+";
        }

        TimeSpan timePlayed = TimeSpan.FromSeconds(UserData.GetFloat(UserData.totalTimePlayed));
        timePlayedValue.text = timePlayed.ToString("hh':'mm':'ss");
    }

    private void SetValue(string key, TextMeshProUGUI txtObj)
    {
        if(UserData.GetInt(key) < 10000)
        {
            txtObj.text = UserData.GetInt(key).ToString();
        }
        else
        {
            txtObj.text = "9999+";
        }
    }
}
