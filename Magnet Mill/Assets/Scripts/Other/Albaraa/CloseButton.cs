using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public enum TargetMenu {LevelSelection, Statistics};
    public TargetMenu whichMenu;
    public void ButtonClicked()
    {
        if (whichMenu == TargetMenu.LevelSelection)
        {
            if (LevelButton.btnClicked) return;
            MainMenuBtn.BtnClicked = false;
            transform.parent.parent.gameObject.SetActive(false);
        }
    }

}