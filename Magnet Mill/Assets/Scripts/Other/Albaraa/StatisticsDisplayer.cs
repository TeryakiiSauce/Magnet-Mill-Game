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
        TimeSpan timePlayed = TimeSpan.FromSeconds(UserData.GetFloat(UserData.totalTimePlayed));

        levelsFinishedValue.text = UserData.GetInt(UserData.numOfLevelsFinished).ToString();
        cubeRolledValue.text = UserData.GetInt(UserData.numOfCubeRolled).ToString();
        gravitySwitValue.text = UserData.GetInt(UserData.numOfGravitySwitched).ToString();
        checkpointActValue.text = UserData.GetInt(UserData.numOfCheckpointsActivated).ToString();
        abilitiesValue.text = UserData.GetInt(UserData.numOfAbilitiesUsed).ToString();
        deathsValue.text = UserData.GetInt(UserData.numOfDeaths).ToString();
        totalScoreValue.text = UserData.GetInt(UserData.numOfTotalScore).ToString();
        timePlayedValue.text = timePlayed.ToString("hh':'mm':'ss");
        level1HighScoreValue.text = UserData.GetInt(UserData.level1HighScore).ToString();
        level2HighScoreValue.text = UserData.GetInt(UserData.level2HighScore).ToString();
        level3HighScoreValue.text = UserData.GetInt(UserData.level3HighScore).ToString();
        level4HighScoreValue.text = UserData.GetInt(UserData.level4HighScore).ToString();
    }


}
