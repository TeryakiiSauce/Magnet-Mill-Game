using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOffBoundHandler : MonoBehaviour
{
    private RaycastHit groundHit;
    private float timer;
    public bool isoutOfBounds = false;
    public static OutOffBoundHandler instance;
    private bool IsGridTag(string tag)
    {
        return tag == "GroundCorner" || tag == "Ground" || tag == "RightWallCorner"
            || tag == "Right wall" || tag == "RoofCorner" || tag == "Roof" || tag == "leftWallCorner"
            || tag == "Left wall";
    }

    public bool checkOutOfBounds(Vector3 postion) 
    {
        Vector3 direction = Vector3.zero; ;
        if (GameController.instance.IsInGround())//onGround
        {
            direction = Vector3.down;
        }
        else if (GameController.instance.IsInRoof())//on roof
        {
            direction = Vector3.up;
        }
        else if (GameController.instance.IsInRight())//on right wall
        {
            direction = Vector3.right;
        }
        else if (GameController.instance.IsInLeft())//on left wall
        {
            direction = Vector3.left;
        }
        Ray surfaceOnRay = new Ray(postion, direction);
        if (Physics.Raycast(surfaceOnRay, out groundHit, 1.5f))
        {
            if (IsGridTag(groundHit.transform.tag))
            {
                return true;
            }
        }
        return false;
    }

    public void setTimer()
    {
        if (isoutOfBounds)
        {
            if (timer <= 0.35)
            {
                if (!GameController.instance.IsDead())
                {
                    GameController.instance.PlayerDead();
                }
                timer += Time.deltaTime;
            }
            else if (timer < 1 && timer >= 0.35)
            { 
                timer += Time.deltaTime;
            }
            else
            {
                GameController.instance.OutOfMap();
                isoutOfBounds = false;
                timer = 0;
            }
        }
    }
}
