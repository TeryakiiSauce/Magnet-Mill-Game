using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointGrid : MonoBehaviour
{
    private GameObject previousCheckPoint;
    private MeshRenderer thisMRenderer;
    private bool isChecked;
    void Start()
    {
        if(gameObject.name[gameObject.name.Length - 1] != '0')
        {
            int prevIND = Convert.ToInt32(char.GetNumericValue(gameObject.name[gameObject.name.Length - 1]));
            prevIND--;
            string checkPointName = "Checkpoint" + prevIND;
            
            previousCheckPoint = GameObject.Find(checkPointName);
        }
        thisMRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isChecked || other.tag != "PlayerCube") return;     //Check if collided object is not playercube or the check point is already active, if yes return
        thisMRenderer.material = GameController.instance.checkPointOnMaterial;  //Taking "ON" material from game controller object
        GameController.instance.SetCheckPoint(gameObject.tag);  //Set checkpoint as a current checkpoint
        isChecked = true;
        if(previousCheckPoint != null)
        {
            previousCheckPoint.GetComponent<CheckPointGrid>().Deactivate();
        }
    }

    public void Deactivate()
    {

    }
}
