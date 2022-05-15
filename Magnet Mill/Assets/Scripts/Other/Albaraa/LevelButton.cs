using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public static bool btnClicked;
    public Image frameImg;
    public Image txtImg;
    public Sprite highlightedFrameSP;
    public Color highlightedTxtColor;
    private Sprite nrmlFrameSP;
    private Color nrmlTxtColor;
    void Start()
    {
        btnClicked = false;
        nrmlFrameSP = frameImg.sprite;
        nrmlTxtColor = txtImg.color;
    }

    public void MouseOnLevel()
    {
        if (btnClicked) return;
        if (frameImg.sprite == nrmlFrameSP) frameImg.sprite = highlightedFrameSP;
        if (txtImg.color == nrmlTxtColor) txtImg.color = highlightedTxtColor;
    }

    public void MouseNotOnLevel()
    {
        if (btnClicked) return;
        if (frameImg.sprite == highlightedFrameSP) frameImg.sprite = nrmlFrameSP;
        if (txtImg.color == highlightedTxtColor) txtImg.color = nrmlTxtColor;
    }

    public void ButtonClicked()
    {
        if (btnClicked) return;
        LevelInSelectionMenu.WhichLevel thisLevel = transform.parent.GetComponent<LevelInSelectionMenu>().currentLevel;
        if (thisLevel == LevelInSelectionMenu.WhichLevel.Level1)
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level1);
        }
        else if (thisLevel == LevelInSelectionMenu.WhichLevel.Level2)
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level2);
        }
        else if (thisLevel == LevelInSelectionMenu.WhichLevel.Level3)
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level3);
        }
        else if (thisLevel == LevelInSelectionMenu.WhichLevel.Level4)
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level4);
        }
        btnClicked = true;
    }
}
