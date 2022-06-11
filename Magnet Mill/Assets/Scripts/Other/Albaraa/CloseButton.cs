using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public enum TargetMenu {LevelSelection, Statistics};
    public TargetMenu whichMenu;
    public void ButtonClicked()     //will be called when close button clicked
    {
        if (whichMenu == TargetMenu.LevelSelection)
        {
            if (LevelButton.btnClicked) return;
        }
        else
        {
            StatisticsButton.isClicked = false;
        }
        MainMenuBtn.BtnClicked = false;
        transform.parent.parent.gameObject.SetActive(false);
    }

}
