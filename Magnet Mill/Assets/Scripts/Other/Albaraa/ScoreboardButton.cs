using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardButton : MonoBehaviour
{
    public enum WhichButton { MainMenu, Restart, Next};
    public WhichButton currentBtn;
    private static bool btnClicked;
    void Start()
    {
        btnClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked()
    {
        if (btnClicked || !Scoreboard.ScoreCalculated) return;  //if any of the scoreboard buttons clicked or the score not yet calculated return
        if(currentBtn == WhichButton.MainMenu)
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.MainMenu);    //move to main menu since the current button is main menu button
        }
        else if(currentBtn == WhichButton.Restart)
        {
            ScenesLoader.instance.ReloadScene();    //Restart level
        }
        else         //Next button clicked
        {
            if(GameController.instance.currentLevel == "Level1")
            {
                ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level2);
            }
            else if(GameController.instance.currentLevel == "Level2")
            {
                ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level3);
            }
            else if (GameController.instance.currentLevel == "Level3")
            {
                ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level4);
            }
            else if(GameController.instance.currentLevel == "Level4")
            {
                ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.MainMenu);
            }
        }
        btnClicked = true;
    }
}
