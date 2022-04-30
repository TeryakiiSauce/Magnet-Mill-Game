using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    public GameObject mapBox;
    public GameObject mapKeys;
    public GameObject boostAbility;
    public GameObject jumpAbility;
    public GameObject freezeAbility;
    public Sprite groundKeys;
    public Sprite roofKeys;
    public Sprite rightKeys;
    public Sprite leftKeys;
    public Sprite boostAvailable;
    public Sprite boostDisabled;
    public Sprite boostCooldown;
    public Sprite jumpAvailable;
    public Sprite jumpDisabled;
    public Sprite jumpCooldown;
    public Sprite freezeAvailable;
    public Sprite freezeDisabled;
    public Sprite freezeCooldown;
    public RectTransform rect;
    [HideInInspector] public bool rotateSquare;
    [HideInInspector] public bool angleSet = false;
    private Vector3 tempQ;
    private float rotationEndAngle = 0;
    private int rotationSpeed = 140;

    private void Awake()
    {
        if (instance == null)    //checking if instance is null or not becuase we only need one instance of this class
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        if (!rotateSquare) return;
        Rotate();
    }


    private void Rotate()
    {

        SetRotationEndAngle();
        tempQ = rect.eulerAngles;

        if (tempQ.z < rotationEndAngle)
        {
            tempQ.z += rotationSpeed * Time.deltaTime;
            rect.eulerAngles = tempQ;

            if (Mathf.Approximately(tempQ.z, rotationEndAngle) || tempQ.z >= rotationEndAngle)
            {
                rotateSquare = false;
                tempQ.z = rotationEndAngle; // set the exact angle without any fractions
                rect.eulerAngles = tempQ;
            }

        }
        else if (tempQ.z > rotationEndAngle)
        {
            tempQ.z -= rotationSpeed * Time.deltaTime;
            rect.eulerAngles = tempQ;

            if (Mathf.Approximately(tempQ.z, rotationEndAngle) || tempQ.z <= rotationEndAngle)
            {
                rotateSquare = false;
                tempQ.z = rotationEndAngle; // set the exact angle without any fractions
                rect.eulerAngles = tempQ;
            }
        }
        else
        {
            rotateSquare = false;
        }


    }
    

    private void SetRotationEndAngle()
    {
        if (!angleSet)
        {
            if (RightToGround() || LeftToGround() || RoofToGround())
            {
                rotationEndAngle = 0;
            }
            else if (GroundToRight() || RoofToRight() || LeftToRight())
            {
                rotationEndAngle = 90;
            }
            else if (RightToRoof() || LeftToRoof() || GroundToRoof())
            {
                rotationEndAngle = 180;
            }
            else if (GroundToLeft() || RoofToLeft() || RightToLeft())
            {
                rotationEndAngle = 270;
            }

            angleSet = true;

        }
    }

    private bool GroundToRight()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Ground
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Right;
    }

    private bool GroundToRoof()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Ground
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Roof;
    }


    private bool GroundToLeft()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Ground
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Left;
    }

    private bool RightToRoof()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Right
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Roof;
    }

    private bool RightToLeft()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Right
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Left;
    }

    private bool RightToGround()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Right
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Ground;
    }

    private bool RoofToLeft()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Roof
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Left;
    }

    private bool RoofToRight()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Roof
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Right;
    }

    private bool RoofToGround()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Roof
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Ground;
    }

    private bool LeftToGround()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Left
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Ground;
    }

    private bool LeftToRoof()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Left
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Roof;
    }

    private bool LeftToRight()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Left
            && GameController.instance.currentMagnetPosition == GameController.CheckDirection.Right;
    }

    private void ChangeImage(GameObject obj, Sprite sprite)
    {
        obj.GetComponent<Image>().sprite = sprite;
    }

    public void SetGroundKeysImg()
    {
        ChangeImage(mapKeys,groundKeys);
    }

    public void SetRightKeysImg()
    {
        ChangeImage(mapKeys, rightKeys);
    }

    public void SetRoofKeysImg()
    {
        ChangeImage(mapKeys, roofKeys);
    }

    public void SetLeftKeysImg()
    {
        ChangeImage(mapKeys, leftKeys);
    }

    public void SetBoostAbilityAvailable()
    {
        ChangeImage(boostAbility, boostAvailable);
    }
    
    public void SetBoostAbilityDisabled()
    {
        ChangeImage(boostAbility, boostDisabled);
    }
    public void SetBoostAbilityCooldown()
    {
        ChangeImage(boostAbility, boostCooldown);
    }

    public void SetJumpAbilityAvailable()
    {
        ChangeImage(jumpAbility, jumpAvailable);
    }

    public void SetJumpAbilityDisabled()
    {
        ChangeImage(jumpAbility, jumpDisabled);
    }
    public void SetJumpAbilityCooldown()
    {
        ChangeImage(jumpAbility, jumpCooldown);
    }

    public void SetFreezeAbilityAvailable()
    {
        ChangeImage(freezeAbility, freezeAvailable);
    }

    public void SetFeezeAbilityDisabled()
    {
        ChangeImage(freezeAbility, freezeDisabled);
    }
    public void SetFreezeAbilityCooldown()
    {
        ChangeImage(freezeAbility, freezeCooldown);
    }


}
