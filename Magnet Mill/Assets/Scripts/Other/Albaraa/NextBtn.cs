using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBtn : MonoBehaviour
{
    private bool isClicked;

    public void NextBtnClicked()
    {
        if (isClicked) return;
        ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level1);
        isClicked = true;
    }
}
