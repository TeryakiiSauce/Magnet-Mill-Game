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
    public RectTransform mainMenuBtnRect;
    public RectTransform restartBtnRect;
    public GameObject nextBtn;
    public static bool ScoreCalculated;
    private int ScoreCount;
    private int ScoreTotal;
    private bool belowHalfScore;
    void Start()
    {
        ScoreCalculated = false;
        timerValue.text = TimerController.instance.GetTimeInString();
        deathsValue.text = GameController.instance.deathCount.ToString();
        rolledValue.text = GameController.instance.rollsCount.ToString();
        int decrement = (int)TimerController.instance.GetTimeInFloat() + (GameController.instance.deathCount * 10)
            + GameController.instance.rollsCount;
        ScoreTotal = GameController.instance.levelMaxScore - decrement;
        if(ScoreTotal < 0)
        {
            ScoreTotal = 0;
        }
        if (ScoreTotal < GameController.instance.levelMaxScore / 2)
        {
            nextBtn.SetActive(false);
            mainMenuBtnRect.anchoredPosition= new Vector3(-15f, -53.5f, 1);
            restartBtnRect.anchoredPosition = new Vector3(15f, -53.5f, 1);
            belowHalfScore = true;
        }
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
        if(belowHalfScore)
        {
            scoreValue.color = new Color(0.8f, 0.27f, 0.27f, 1);
        }
        if(GameController.instance.currentLevel == "Level1")
        {
            if (UserData.GetInt(UserData.level1HighScore) < ScoreCount) UserData.SetInt(UserData.level1HighScore, ScoreCount);
            if(!belowHalfScore) UserData.SetBool(UserData.finishedLevel1, true);
        }
        else if (GameController.instance.currentLevel == "Level2")
        {
            if (UserData.GetInt(UserData.level2HighScore) < ScoreCount) UserData.SetInt(UserData.level2HighScore, ScoreCount);
            if (!belowHalfScore) UserData.SetBool(UserData.finishedLevel2, true);
        }
        else if (GameController.instance.currentLevel == "Level3")
        {
            if (UserData.GetInt(UserData.level3HighScore) < ScoreCount) UserData.SetInt(UserData.level3HighScore, ScoreCount);
            if (!belowHalfScore) UserData.SetBool(UserData.finishedLevel3, true);
        }
        else if (GameController.instance.currentLevel == "Level4")
        {
            if (UserData.GetInt(UserData.level4HighScore) < ScoreCount) UserData.SetInt(UserData.level4HighScore, ScoreCount);
            if (!belowHalfScore) UserData.SetBool(UserData.finishedLevel4, true);
        }
        //Display grade
        ScoreCalculated = true;
        enabled = false;
    }
}
