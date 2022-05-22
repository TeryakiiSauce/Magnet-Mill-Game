using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public Canvas pauseMenu;
    private static bool isMenuOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!pauseMenu)
        {
            Debug.Log("Remove/disable the script \"PauseMenuController.cs\" from the Cursor prefab (DON'T apply override) **IF** you don't have a pause menu in your scene. Otherwise make sure to set the pause menu canvas to the script.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            && !pauseMenu.isActiveAndEnabled && !GameController.instance.IsLevelFinshed())
        {
            OpenMenu();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && pauseMenu.isActiveAndEnabled)
        {
            CloseMenu();
        }
    }

    public static bool IsPauseMenuOpened()
    {
        return isMenuOpened;
    }

    public void OpenMenu()
    {
        //print("menu opened");
        pauseMenu.gameObject.SetActive(true);
        isMenuOpened = true;
        Time.timeScale = 0;

        //pause timer
        TimerController.instance.PauseTimer();
    }

    public void CloseMenu()
    {
        //print("menu closed");
        pauseMenu.gameObject.SetActive(false);
        isMenuOpened = false;
        Time.timeScale = 1;

        //start timer
        TimerController.instance.PlayTimer();
    }

    public void ResumeButtonOnClick()
    {
        CloseMenu();
    }

    public void ReloadButtonOnClick()
    {
        CloseMenu();
        GameController.instance.OutOfMap();
    }

    public void RestartButtonOnClick()
    {
        CloseMenu();
        ScenesLoader.instance.ReloadScene();
    }

    public void MainMenuButtonOnClick()
    {
        CloseMenu();
        ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.MainMenu);
    }

    public void ExitButtonOnClick()
    {
        Application.Quit();
    }
}
