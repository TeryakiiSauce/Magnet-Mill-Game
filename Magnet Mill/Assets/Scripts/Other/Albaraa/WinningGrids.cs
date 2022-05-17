using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningGrids : MonoBehaviour
{
    public GameObject stageFinishedCanvas;
    public GameObject scoreboard;
    public GameObject completedText;
    public GameObject nextBtn;
    public void OnTriggerEnter(Collider other)
    {

        if (GameController.instance.IsLevelFinshed() || other.tag != "PlayerCube") return; //Check if collided object is not cube
        GameController.instance.LevelFinished();                                           //and level not finished, then return
        stageFinishedCanvas.SetActive(true);
        if(GameController.instance.currentLevel != "Level0")
        {
            scoreboard.SetActive(true);
        }
        else
        {
            Image pnlImg = completedText.transform.parent.GetComponent<Image>();
            pnlImg.color = new Color(pnlImg.color.r, pnlImg.color.g, pnlImg.color.b, 0.3f);
            completedText.SetActive(true);
            nextBtn.SetActive(true);
        }

    }
}
