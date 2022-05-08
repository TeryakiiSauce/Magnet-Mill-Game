using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOffBoundHandler : MonoBehaviour
{
    public float timer;
    public bool isOnObject;
    void Update()
    {
        if (!isOnObject)
        {
            outOfBounds();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!CubeController.flipinggravity && IsGridTag(other.tag))
        {
            isOnObject = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    }
    private void OnTriggerStay(Collider other)
    {

        if (IsGridTag(other.tag))
        {
            isOnObject = true;
            timer = 0;
            CubeController.isTouchingGround = true;
        }
    }

    private void outOfBounds()
    {
         if (timer < 0.3)
        {
            
            timer += Time.deltaTime;
        }
        else
        {
            CubeController.isTouchingGround = false;
            GameController.instance.OutOfMap();
            timer = 0;
        }
    }

    private bool IsGridTag(string tag)
    {
        return tag == "GroundCorner" || tag == "Ground" || tag == "RightWallCorner"
            || tag == "Right wall" || tag == "RoofCorner" || tag == "Roof" || tag == "leftWallCorner"
            || tag == "Left wall";
    }

}
