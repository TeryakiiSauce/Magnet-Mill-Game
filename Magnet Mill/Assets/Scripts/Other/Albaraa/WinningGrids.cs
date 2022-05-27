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
    [SerializeField] ParticleSystem finishParticle = null;

    public void OnTriggerEnter(Collider other)
    {
        if (GameController.instance.IsLevelFinshed() || other.tag != "PlayerCube") return; //Check if collided object is not cube
        finishParticle.Play();                                                             //and level not finished, then return
        GameController.instance.LevelFinished();                                          
        stageFinishedCanvas.SetActive(true); 
        if(GameController.instance.currentLevel != "Level0")    //if level not 0 enable the scoreboard
        {
            scoreboard.SetActive(true);
        }
        else
        {
            AudioManager.instance.Play("Winning");
            Image pnlImg = completedText.transform.parent.GetComponent<Image>();
            pnlImg.color = new Color(pnlImg.color.r, pnlImg.color.g, pnlImg.color.b, 0.75f);
            completedText.SetActive(true);  //Enable "Tutoral compeleted" text
            nextBtn.SetActive(true);    //Enable next button
        }

    }
}
