using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningGrids : MonoBehaviour
{
    public GameObject stageFinishedCanvas;

    public void OnTriggerEnter(Collider other)
    {

        if (GameController.instance.IsLevelFinshed() || other.tag != "PlayerCube") return; //Check if collided object is not cube
                                                                                           //and level not finished, then return
        stageFinishedCanvas.SetActive(true);
        GameController.instance.LevelFinished();

    }
}
