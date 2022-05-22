using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointGrid : MonoBehaviour
{
    [SerializeField] ParticleSystem particle = null;
    private CheckPointGrid previousCheckPointScript;
    private MeshRenderer thisMRenderer;
    private bool isChecked;
    void Start()
    {
        particle.Play();
        if (gameObject.name[gameObject.name.Length - 1] != '0')  //check if this point is not the spawning point
        {
            int prevIND = Convert.ToInt32(char.GetNumericValue(gameObject.name[gameObject.name.Length - 1]));   //take current index by char and convert it to int
            prevIND--;      //current index - 1 to get the previous grid
            string checkPointName = "Checkpoint" + prevIND;
            
            GameObject previousCheckPoint = GameObject.Find(checkPointName);    //find the previous grid from the scene by its name
            previousCheckPointScript = previousCheckPoint.GetComponent<CheckPointGrid>();   //assign previous grid script to "previousCheckPointScript"
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
        particle.Stop();
        thisMRenderer.material = GameController.instance.checkPointOnMaterial;  //Taking "ON" material from game controller object
        GameController.instance.SetCheckPoint(gameObject.tag);  //Set checkpoint as a current checkpoint
        isChecked = true;       //Set to true, so the cube when moved aver this grid again will ignore this function by "return"
        if(previousCheckPointScript != null)    //if the previous grid script is null it means that this grid is the spawning point
        {
            UserData.IncrementInt(UserData.numOfCheckpointsActivated);
            previousCheckPointScript.Deactivate();
        }
    }

    public void Deactivate()    //this function to deactivate the checkpoint if it is still not colided and the user took another
    {                           //checkpoint and that checkpoint is closer to the finish line than this checkpoint
        if (isChecked) return;
        particle.Stop();
        thisMRenderer.material = GameController.instance.checkPointOnMaterial;
        isChecked = true;
        previousCheckPointScript.Deactivate();  //call the previous, to create recursion that will deactivate all previous points
    }
}
