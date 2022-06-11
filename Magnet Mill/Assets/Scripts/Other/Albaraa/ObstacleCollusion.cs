using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollusion : MonoBehaviour
{
    public bool disableWhenFar;
    private Collider thisCol;
    private bool collidedWithPlayer;
    private float timer;
    void Start()
    {
        thisCol = GetComponent<Collider>();
    }

    void Update()   //in update we will check if the obstacle is near the user, if yes we will turn on the collider,
    {               //if no we will turn off the collider, this will save memory
        if(collidedWithPlayer)
        {
            if(timer<2f)    //wait for two seconds after collision then move to the last checkpoint
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0f;
                GameController.instance.OutOfMap();
                collidedWithPlayer = false;
            }
        }
        if (!disableWhenFar) return;    //if the collider is not meant be disables if it was far from player then return
        if(!thisCol.enabled && Vector3.Distance(transform.position, GameController.instance.CubePosition()) <= 5)   //if collider close to player enable it
        {
            thisCol.enabled = true;
        }
        else if(thisCol.enabled && Vector3.Distance(transform.position, GameController.instance.CubePosition()) > 5) //if collider fat from player disable it
        {
            thisCol.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) //Check if the obstacle hit the cube, if yes send a message to the game controller
    {
        if(other.tag == "PlayerCube" && !collidedWithPlayer)
        {
            PlayerHitten();
        }
    }

    private void OnCollisionEnter(Collision collision)  //used both events so it is working for any type of colliders, whether trigger or not 
    {
        if (collision.gameObject.tag == "PlayerCube" && !collidedWithPlayer)
        {
            PlayerHitten();
        }
    }

    private void PlayerHitten()
    {
        GameController.instance.PlayerDead();   //lock controls
        collidedWithPlayer = true;
    }
}
