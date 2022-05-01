using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOffBoundHandler : MonoBehaviour
{
    private float timer;
    void Update()
    {
        if(GameController.instance.IsDead())    //check if player is dead
        {
            if(timer <1.5f)     //wait for 1.5 seconds then execute OutOfMap function from gamecontroller
            {
                timer += Time.deltaTime;
            }
            else
            {
                GameController.instance.OutOfMap();
                timer = 0;
            }
        }
    }

}
