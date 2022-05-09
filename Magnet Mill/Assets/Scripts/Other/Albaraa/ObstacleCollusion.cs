using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollusion : MonoBehaviour
{
    private Collider thisCol;

    void Start()
    {
        thisCol = GetComponent<Collider>();
    }

    void Update()   //in update we will check if the obstacle is near the user, if yes we will turn on the collider,
    {               //if no we will turn off the collider, this will save memory
        if(!thisCol.enabled && Vector3.Distance(transform.position, GameController.instance.CubePosition()) <= 3)
        {
            thisCol.enabled = true;
        }
        else if(thisCol.enabled && Vector3.Distance(transform.position, GameController.instance.CubePosition()) > 3)
        {
            thisCol.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) //Check if the obstacle hit the cube, if yes send a message to the game controller
    {
        if(other.tag == "PlayerCube")
        {
            GameController.instance.OutOfMap();
        }
    }
}
