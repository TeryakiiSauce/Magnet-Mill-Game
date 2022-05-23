using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController instance;
    public RectTransform playRect;
    public GameObject tutorialBtn;
    public RectTransform instructionsRect;
    public RectTransform creditsRect;
    public RectTransform exitRect;
    public bool resetAbilities;
    public bool resetLevels;
    public bool resetStatistics;
    private float[] yPositions = {270, 90, -90, -270};  //these variables will be used to place the buttons if the tutorial button is not active
    void Awake()
    {
        if (instance == null)   //checking if instance is null or not because we only need one instance of this class
        {
            instance = this;    
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        AudioManager.instance.Stop("Level1Theme");
        AudioManager.instance.Stop("Level2Theme");
        AudioManager.instance.Stop("Level3Theme");
        AudioManager.instance.Stop("Level4Theme");
        if(!AudioManager.instance.IsPlaying("MainMenuTheme")) AudioManager.instance.Play("MainMenuTheme");
        if (AudioManager.instance.GetVolume("MainMenuTheme") == 0.2f) AudioManager.instance.AddVolume("MainMenuTheme", 0.2f);
        if (resetAbilities)
        {
            UserData.SetBool(UserData.speedCollected, false);
            UserData.SetBool(UserData.jumpCollected, false);
            UserData.SetBool(UserData.freezeCollected, false);
        }
        if(resetLevels)
        {
            UserData.SetBool(UserData.finishedTutorial, false);
            UserData.SetBool(UserData.finishedLevel1, false);
            UserData.SetBool(UserData.finishedLevel2, false);
            UserData.SetBool(UserData.finishedLevel3, false);
            UserData.SetBool(UserData.finishedLevel4, false);
        }
        if(resetStatistics)
        {
            UserData.ResetStatistics();
        }
        if(!UserData.GetBool(UserData.finishedTutorial))    //Checking if the user finished the tutorial, if not then deactivate the                                                       
        {                                                   //tutorial button, because the play button will redirect him to the tutorial
            tutorialBtn.SetActive(false);

            Vector3 tempPos = playRect.anchoredPosition;    //Repositioning every button by changing the y position to
            tempPos.y = yPositions[0];                      //maintain the gap of tutorial button, since its deactivated
            playRect.anchoredPosition = tempPos;

            tempPos = instructionsRect.anchoredPosition;
            tempPos.y = yPositions[1];
            instructionsRect.anchoredPosition = tempPos;

            tempPos = creditsRect.anchoredPosition;
            tempPos.y = yPositions[2];
            creditsRect.anchoredPosition = tempPos;

            tempPos = exitRect.anchoredPosition;
            tempPos.y = yPositions[3];
            exitRect.anchoredPosition = tempPos;
        }
    }

}
