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
        //get time, deaths and number of cube rolled in order to display them in the scoreboard
        timerValue.text = TimerController.instance.GetTimeInString();   
        deathsValue.text = GameController.instance.deathCount.ToString();
        rolledValue.text = GameController.instance.rollsCount.ToString();
        int decrement = (int)TimerController.instance.GetTimeInFloat() + (GameController.instance.deathCount * 20)
            + GameController.instance.rollsCount;   //Counting the final score
        ScoreTotal = GameController.instance.levelMaxScore - decrement; //levelMaxScore will be different for each level that's why we are taking it from the game controller
        if(ScoreTotal < 0)  //handling if the total score is less than zero
        {
            ScoreTotal = 0;
        }
        if (ScoreTotal < GameController.instance.levelMaxScore / 2) //if total score is less than the half of the max score it means
        {                                                           //the user lost, so we will hide the next button
            AudioManager.instance.Play("Failed");
            nextBtn.SetActive(false);
            mainMenuBtnRect.anchoredPosition= new Vector3(-15f, -53.5f, 1); //Reposition main menu button
            restartBtnRect.anchoredPosition = new Vector3(15f, -53.5f, 1);  //Reposition restart button
            belowHalfScore = true;  //Prevent next level to be unlocked
        }
        else
        {
            AudioManager.instance.Play("Winning");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreCount < ScoreTotal) //increase scorecoiunter if it is less than the actual score
        {

            ScoreCount+=5;
            scoreValue.text = ScoreCount.ToString();
        }
        else
        {
            SetToFinalScore();
        }
    }

    public void ShowScoreInstantly()    //Will be called if the user clicked the scoreboard (skip counting)
    {
        SetToFinalScore();
    }

    private void SetToFinalScore()
    {
        GetComponent<Button>().enabled = false;
        ScoreCount = ScoreTotal;
        scoreValue.text = ScoreCount.ToString();
        UserData.IncrementInt(UserData.numOfTotalScore, ScoreCount);    //increment the total score of all games
        if(belowHalfScore)
        {
            scoreValue.color = new Color(0.8f, 0.27f, 0.27f, 1);    //changing score color to red (player lost)
        }
        if(GameController.instance.currentLevel == "Level1")
        {
            if (UserData.GetInt(UserData.level1HighScore) < ScoreCount) UserData.SetInt(UserData.level1HighScore, ScoreCount);
            if(!belowHalfScore) UserData.SetBool(UserData.finishedLevel1, true);    //unlock next level if the player won
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
        ScoreCalculated = true;
        enabled = false;
    }
}
