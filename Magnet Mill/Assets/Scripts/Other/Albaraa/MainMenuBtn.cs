using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBtn : MonoBehaviour
{
    public enum MenuBtn {play, tutorial, instruction, credits, exit};
    public MenuBtn whichButton;
    public GameObject activateGameobject;
    public static bool BtnClicked;
    void Start()
    {
        BtnClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked()
    {
        if (ScenesLoader.instance.IsTransitioning() || BtnClicked) return;

        if(whichButton == MenuBtn.play)
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level0);
        }
        else if(whichButton == MenuBtn.tutorial)
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level0);
        }
        else if(whichButton == MenuBtn.instruction)
        {
            activateGameobject.SetActive(true);
        }
        else if(whichButton == MenuBtn.credits)
        {
            activateGameobject.SetActive(true);           
        }
        else
        {
            Application.Quit();
        }
        BtnClicked = true;
    }

}
