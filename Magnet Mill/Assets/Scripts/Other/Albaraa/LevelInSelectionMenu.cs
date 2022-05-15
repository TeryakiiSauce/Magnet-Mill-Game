using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInSelectionMenu : MonoBehaviour
{
    public enum WhichLevel {Level1, Level2, Level3, Level4};
    public WhichLevel currentLevel;
    public Sprite onSprite;
    public GameObject levelBtn;
    public GameObject levelLock;

    void Start()
    {
        if(currentLevel == WhichLevel.Level1)
        {
            if (UserData.GetBool(UserData.finishedTutorial)) SetEnabled();
        }
        else if(currentLevel == WhichLevel.Level2)
        {
            if (UserData.GetBool(UserData.finishedLevel1)) SetEnabled();
        }
        else if(currentLevel == WhichLevel.Level3)
        {
            if (UserData.GetBool(UserData.finishedLevel2)) SetEnabled();
        }
        else if (currentLevel == WhichLevel.Level4)
        {
            if (UserData.GetBool(UserData.finishedLevel3)) SetEnabled();
        }
    }

    private void SetEnabled()
    {
        levelLock.SetActive(false);
        GetComponent<Image>().sprite = onSprite;
        levelBtn.SetActive(true);
    }
}
