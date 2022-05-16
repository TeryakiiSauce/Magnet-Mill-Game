using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBtn : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextBtnClicked()
    {
        if(GameController.instance.currentLevel == "Level0")
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level1);
        }
        else if(GameController.instance.currentLevel == "Level1")
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level2);
        }
        else if (GameController.instance.currentLevel == "Level2")
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level3);
        }
        else if (GameController.instance.currentLevel == "Level3")
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level4);
        }
        else
        {
            print("In else block!");
        }
    }
}
