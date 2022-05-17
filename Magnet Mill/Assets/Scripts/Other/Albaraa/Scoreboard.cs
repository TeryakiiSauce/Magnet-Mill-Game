using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public TextMeshProUGUI timerValue;
    public TextMeshProUGUI deathsValue;
    public TextMeshProUGUI rolledValue;
    public TextMeshProUGUI scoreValue;
    private int ScoreCount;
    private int ScoreTotal;
    void Start()
    {
        timerValue.text = TimerController.instance.GetTimeInString();
        deathsValue.text = GameController.instance.deathCount.ToString();
        rolledValue.text = GameController.instance.rollsCount.ToString();
        int decrement = (int)TimerController.instance.GetTimeInFloat() + (GameController.instance.deathCount * 10)
            + GameController.instance.rollsCount;
        ScoreTotal = GameController.instance.levelMaxScore - decrement;
    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreCount < ScoreTotal)
        {

            ScoreCount+=5;
            scoreValue.text = ScoreCount.ToString();
        }
        else
        {
            SetToFinalScore();
        }
    }

    public void ShowScoreInstantly()
    {
        SetToFinalScore();
    }

    private void SetToFinalScore()
    {
        GetComponent<Button>().enabled = false;
        ScoreCount = ScoreTotal;
        scoreValue.text = ScoreCount.ToString();
        UserData.IncrementInt(UserData.numOfTotalScore, ScoreCount);
        if(GameController.instance.currentLevel == "Level1")
        {
            if (UserData.GetInt(UserData.level1HighScore) < ScoreCount) UserData.SetInt(UserData.level1HighScore, ScoreCount);
        }
        else if (GameController.instance.currentLevel == "Level2")
        {
            if (UserData.GetInt(UserData.level2HighScore) < ScoreCount) UserData.SetInt(UserData.level2HighScore, ScoreCount);
        }
        else if (GameController.instance.currentLevel == "Level3")
        {
            if (UserData.GetInt(UserData.level3HighScore) < ScoreCount) UserData.SetInt(UserData.level3HighScore, ScoreCount);
        }
        else if (GameController.instance.currentLevel == "Level4")
        {
            if (UserData.GetInt(UserData.level4HighScore) < ScoreCount) UserData.SetInt(UserData.level4HighScore, ScoreCount);
        }
        //Disaplay grade
        enabled = false;
    }
}
