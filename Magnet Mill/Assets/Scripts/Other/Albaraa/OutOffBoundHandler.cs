using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOffBoundHandler : MonoBehaviour
{
    private float timer;
    void Update()
    {
        if(CubeController.outOfBounds)
        {
            if(timer <1.5f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                GameController.instance.OutOffMap();
                timer = 0;
            }
        }
    }

}
