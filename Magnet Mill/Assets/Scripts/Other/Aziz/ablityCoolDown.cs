using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private float coolDownTime;
    private float abilityTime;
    private float activeTime;
    private float coolDownEndTime;
    private activeAblity ablityused;
    //enum to check which ablity is active 
    private enum activeAblity
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
        if (Time.time > coolDownEndTime)
        {
            // if the e button is clicked
            if (Input.GetKey(KeyCode.E))
            {
                //setting the cooldown time and the active time  
                coolDownTime = 7f;
                abilityTime = 3f;
                ablityused = activeAblity.Jump;
                /* * change image to cooldown image here * */
            }

            // if the q button is clicked
            else if (Input.GetKey(KeyCode.Q))
            {
                //setting the cooldown time and the active time 
                coolDownTime = 15f;
                abilityTime = 5f;
                ablityused = activeAblity.Freeze;
                /* * change image to cooldown image here * */
            }

            // if the shift button is clicked
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                //setting the cooldown time and the active time 
                coolDownTime = 10f;
                abilityTime = 4f;
                ablityused = activeAblity.Speed;
                /* * change image to active image here * */
            }
            //setting the end time to the cooldown time + the current time  
            coolDownEndTime = Time.time + coolDownTime;
            activeTime = Time.time + abilityTime;
        }
        else 
        {
            if (Time.time < activeTime)
            {
                //setting the image to active
                activeAbility(ablityused);
            }
            else 
            {
                //reseting the image
                resetAbility(ablityused);
                
            }
            
        }
    }

    private void resetAbility(activeAblity AB)
    {
        //checking for the active ability 
        switch (AB)
        {
            case activeAblity.Freeze:
                {
                    /* * change image to ready to use image here * */
                    break;
                }
            case activeAblity.Jump:
                {
                    /* * change image to ready to use image here * */
                    break;
                }
            case activeAblity.Speed:
                {
                    /* * change image to ready to use image here * */
                    break;
                }
            default: break;
        }
        //changing the enum to none
        ablityused = activeAblity.None;
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
