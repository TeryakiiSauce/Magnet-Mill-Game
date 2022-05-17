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
        if (btnClicked || !Scoreboard.ScoreCalculated) return;
        if(currentBtn == WhichButton.MainMenu)
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.MainMenu);
        }
        else if(currentBtn == WhichButton.Restart)
        {
            ScenesLoader.instance.ReloadScene();
        }
        else
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
