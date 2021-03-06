using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCooldown : MonoBehaviour
{
    private float freezeActiveTime;
    private float jumpActiveTime;
    private float speedActiveTime;
    private float freezeCoolDownEndTime;
    private float jumpCoolDownEndTime;
    private float speedCoolDownEndTime;
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

    private void Start()
    {
        HUDController.instance.SetFreezeAbilityAvailable();
        HUDController.instance.SetJumpAbilityAvailable();
        HUDController.instance.SetBoostAbilityAvailable();
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
                float coolDownTime = 10f;
                float abilityTime = 3f;
                jumpAblityused = activeAblity.Jump;
                jumpCoolDownEndTime = Time.time + coolDownTime;
                jumpActiveTime = Time.time + abilityTime;

                /* * change image to cooldown image here * */

            }
        }

        if (Time.time > freezeCoolDownEndTime)
        {
            // if the q button is clicked
            if (Input.GetKey(KeyCode.Q) && !CubeController.isMoving)
            {
                //setting the cooldown time and the active time 
                float coolDownTime = 20f;
                float abilityTime = 5f;
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
                float coolDownTime = 14f;
                float abilityTime = 4f;
                speedAblityused = activeAblity.Speed;
                speedCoolDownEndTime = Time.time + coolDownTime;
                speedActiveTime = Time.time + abilityTime;
                /* * change image to active image here * */

            }
        }

        //setting the end time to the cooldown time + the current time  
        changeAbilityState();



    }



    private void changeAbilityState()
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
        else
        {
            HUDController.instance.SetJumpAbilityAvailable();
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
        else
        {
            HUDController.instance.SetFreezeAbilityAvailable();
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
        else
        {
            HUDController.instance.SetBoostAbilityAvailable();
        }
    }
    private void resetAbility(activeAblity AB)
    {
        //checking for the active ability 
        if (AB == activeAblity.Freeze)
        {
            freezeAblityused = activeAblity.None;
            /* * change image to ready to use image here * */

            HUDController.instance.SetFreezeAbilityCooldown();
        }
        else if (AB == activeAblity.Jump)
        {
            jumpAblityused = activeAblity.None;
            /* * change image to ready to use image here * */
            HUDController.instance.SetJumpAbilityCooldown();
        }
        else if (AB == activeAblity.Speed)
        {
            speedAblityused = activeAblity.None;
            /* * change image to ready to use image here * */
            HUDController.instance.SetBoostAbilityCooldown();
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
                    HUDController.instance.SetFreezeAbilityActive();
                    break;
                }
            case activeAblity.Jump:
                {
                    /* * change image to active to use image here * */

                    HUDController.instance.SetJumpAbilityActive();
                    break;
                }
            case activeAblity.Speed:
                {
                    /* * change image to active to use image here * */

                    HUDController.instance.SetBoostAbilityActive();
                    break;
                }
            default: break;
        }
    }
}