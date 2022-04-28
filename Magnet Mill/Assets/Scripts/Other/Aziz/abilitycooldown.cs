using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilitycooldown : MonoBehaviour
{

    private float coolDownTime;
    private float abilityTime;
    private float freezeActiveTime;
    private float jumpActiveTime;
    private float speedActiveTime;
    private float freezeCoolDownEndTime;
    private float jumpCoolDownEndTime;
    private float speedCoolDownEndTime;
    private bool jumpActive = false;
    public static activeAblity freezeAblityused;
    public static activeAblity speedAblityused;
    public static activeAblity jumpAblityused;

    //enum to check which ablity is active 
    public enum activeAblity
     {
        Freeze,
        Jump,
        Speed,
        None
    }
 
 

    // Update is called once per frame
    void Update()
    {
        //checking if the cooldown end time is less than the current time 
        if (Time.time > jumpCoolDownEndTime) 
        {
            // if the e button is clicked
            if (Input.GetKey(KeyCode.E) && !CubeController.isMoving)
            {
                //setting the cooldown time and the active time  
                coolDownTime = 10f;
                abilityTime = 7f;
                jumpAblityused = activeAblity.Jump;
                jumpCoolDownEndTime = Time.time + coolDownTime;
                jumpActiveTime = Time.time + abilityTime;
                jumpActive = true;
                /* * change image to cooldown image here * */

            }
        }

        if (Time.time > freezeCoolDownEndTime)
        {
            // if the q button is clicked
             if (Input.GetKey(KeyCode.Q) && !CubeController.isMoving)
             {
                //setting the cooldown time and the active time 
                coolDownTime = 20f;
                abilityTime = 5f;
                freezeAblityused = activeAblity.Freeze;
                freezeCoolDownEndTime = Time.time + coolDownTime;
                freezeActiveTime = Time.time + abilityTime;
                /* * change image to cooldown image here * */
            }
        }

        if (Time.time > speedCoolDownEndTime)
        {
            // if the shift button is clicked
            if (Input.GetKey(KeyCode.LeftShift) && !CubeController.isMoving)
            {
                //setting the cooldown time and the active time 
                coolDownTime = 14f;
                abilityTime = 4f;
                speedAblityused = activeAblity.Speed;
                speedCoolDownEndTime = Time.time + coolDownTime;
                speedActiveTime = Time.time + abilityTime;
                /* * change image to active image here * */
            }
        }

        //setting the end time to the cooldown time + the current time  
        changeAbilityState();

    }
      
    

    private void changeAbilityState ()
    {
        if (Time.time < jumpActiveTime)
        {
            //setting the image to active

            activeAbility(jumpAblityused);
        }
        else if (Time.time < jumpCoolDownEndTime)
        {
            //reseting the image
            resetAbility(jumpAblityused);
        }

        if (Time.time < freezeActiveTime)
        {
            //setting the image to active
            activeAbility(freezeAblityused);

        }
        else if (Time.time < freezeCoolDownEndTime)
        {
            //reseting the image
            resetAbility(freezeAblityused);

        }

        if (Time.time < speedActiveTime)
        {
            //setting the image to active
            activeAbility(speedAblityused);
        }
        else if (Time.time < speedCoolDownEndTime)
        {
            //reseting the image
            resetAbility(speedAblityused);
        }
    }
    private void resetAbility(activeAblity AB)
    {
        //checking for the active ability 
        if (AB == activeAblity.Freeze)
                {   
                    freezeAblityused = activeAblity.None;
                    /* * change image to ready to use image here * */
                }
        else if(AB == activeAblity.Jump && !CubeController.isMoving)
                {
                    jumpAblityused = activeAblity.None;
                    /* * change image to ready to use image here * */     
                }
        else if (AB == activeAblity.Speed)
                {
                    speedAblityused = activeAblity.None;
                    /* * change image to ready to use image here * */
                }
        

    }



    private void activeAbility(activeAblity AB)
    {
        //checking for the active ability 
        switch (AB)
        {
            case activeAblity.Freeze:
                {
                    /* * change image to active to use image here * */
                    break;
                }
            case activeAblity.Jump:
                {
                    /* * change image to active to use image here * */
                    break;
                }
            case activeAblity.Speed:
                {
                    /* * change image to active to use image here * */
                    break;
                }
            default: break;
        }
    }
}
