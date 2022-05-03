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
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && !pauseMenu.isActiveAndEnabled)
        {
            //print("menu opened");
            pauseMenu.gameObject.SetActive(true);
            isMenuOpened = true;
            Time.timeScale = 0;

            //pause timer
            TimerController.instance.PauseTimer();
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && pauseMenu.isActiveAndEnabled)
        {
            //print("menu closed");
            pauseMenu.gameObject.SetActive(false);
            isMenuOpened = false;
            Time.timeScale = 1;

            //start timer
            TimerController.instance.PlayTimer();
        }
    }

    public static bool IsPauseMenuOpened()
    {
        return isMenuOpened;
    }
}
