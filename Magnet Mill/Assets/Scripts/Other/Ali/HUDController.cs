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
    public GameObject freezeTxt;
    public GameObject jumpTxt;
    public GameObject boostTxt;
    public Sprite groundKeys;
    public Sprite roofKeys;
    public Sprite rightKeys;
    public Sprite leftKeys;
    public Sprite boostAvailable;
    public Sprite boostActive;
    public Sprite boostCooldown;
    public Sprite jumpAvailable;
    public Sprite jumpActive;
    public Sprite jumpCooldown;
    public Sprite freezeAvailable;
    public Sprite freezeActive;
    public Sprite freezeCooldown;
    public RectTransform rect;
    [HideInInspector] public bool rotateSquare;
    [HideInInspector] public bool angleSet = false;
    private Vector3 tempQ;
    private Vector3 fakeRotation;
    private float rotationEndAngle = 0;
    private int rotationSpeed = 150;
    private bool increaseAngel;
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
    void Start()
    {
        fakeRotation = rect.eulerAngles;
    }
    void Update()
    {
        if (!rotateSquare) return;
        //Rotate();
        if(!angleSet)
        {
            if(GroundToRight() || RightToRoof() || RoofToLeft() || LeftToGround())
            {
                rotationEndAngle = fakeRotation.z + 90;
            }
            else if(GroundToLeft() || LeftToRoof() || RoofToRight() || RightToGround())
            {
                rotationEndAngle = fakeRotation.z - 90;
            }
            else if(GroundToRoof() || RightToLeft())
            {
                rotationEndAngle = fakeRotation.z + 180;
            }
            else if(RoofToGround() || LeftToRight())
            {
                rotationEndAngle = fakeRotation.z - 180;
            }
            if(rotationEndAngle > fakeRotation.z)
            {
                increaseAngel = true;
            }
            else
            {
                increaseAngel = false;
            }
            angleSet = true;
        }

        if(increaseAngel)
        {
            RotateIncreasingly();
        }
        else
        {
            RotateDecreasingly();
        }

    }

    private void RotateIncreasingly()
    {
        tempQ = fakeRotation;

        if (tempQ.z < rotationEndAngle)
        {
            tempQ.z += rotationSpeed * Time.deltaTime;
            rect.eulerAngles = tempQ;
            fakeRotation = tempQ;
            if (tempQ.z >= rotationEndAngle)
            {
                tempQ.z = rotationEndAngle;
                rect.eulerAngles = tempQ;
                fakeRotation = tempQ;
                rotateSquare = false;
            }
        }
    }

    private void RotateDecreasingly()
    {
        tempQ = fakeRotation;

        if (tempQ.z > rotationEndAngle)
        {
            tempQ.z -= rotationSpeed * Time.deltaTime;
            rect.eulerAngles = tempQ;
            fakeRotation = tempQ;
            if (tempQ.z <= rotationEndAngle)
            {
                tempQ.z = rotationEndAngle;
                rect.eulerAngles = tempQ;
                fakeRotation = tempQ;
                rotateSquare = false;
            }
        }
    }

    public void SetMapAngle()
    {
        fakeRotation.z = rotationEndAngle;
        tempQ = fakeRotation;
        rect.eulerAngles = tempQ;
    }

    //private void Rotate()
    //{

    //    SetRotationEndAngle();
    //    tempQ = rect.eulerAngles;

    //    if (tempQ.z < rotationEndAngle)
    //    {
    //        tempQ.z += rotationSpeed * Time.deltaTime;
    //        rect.eulerAngles = tempQ;

    //        if (Mathf.Approximately(tempQ.z, rotationEndAngle) || tempQ.z >= rotationEndAngle)
    //        {
    //            rotateSquare = false;
    //            tempQ.z = rotationEndAngle; // set the exact angle without any fractions
    //            rect.eulerAngles = tempQ;
    //        }

    //    }
    //    else if (tempQ.z > rotationEndAngle)
    //    {
    //        tempQ.z -= rotationSpeed * Time.deltaTime;
    //        rect.eulerAngles = tempQ;

    //        if (Mathf.Approximately(tempQ.z, rotationEndAngle) || tempQ.z <= rotationEndAngle)
    //        {
    //            rotateSquare = false;
    //            tempQ.z = rotationEndAngle; // set the exact angle without any fractions
    //            rect.eulerAngles = tempQ;
    //        }
    //    }
    //    else
    //    {
    //        rotateSquare = false;
    //    }


    //}
    

    //private void SetRotationEndAngle()
    //{
    //    if (!angleSet)
    //    {
    //        if (RightToGround() || LeftToGround() || RoofToGround())
    //        {
    //            rotationEndAngle = 0;
    //        }
    //        else if (GroundToRight() || RoofToRight() || LeftToRight())
    //        {
    //            rotationEndAngle = 90;
    //        }
    //        else if (RightToRoof() || LeftToRoof() || GroundToRoof())
    //        {
    //            rotationEndAngle = 180;
    //        }
    //        else if (GroundToLeft() || RoofToLeft() || RightToLeft())
    //        {
    //            rotationEndAngle = 270;
    //        }

    //        angleSet = true;

    //    }
    //}

    private bool GroundToRight()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Ground
            && GameController.instance.IsInRight();
    }

    private bool GroundToRoof()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Ground
            && GameController.instance.IsInRoof();
    }


    private bool GroundToLeft()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Ground
            && GameController.instance.IsInLeft();
    }

    private bool RightToRoof()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Right
            && GameController.instance.IsInRoof();
    }

    private bool RightToLeft()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Right
            && GameController.instance.IsInLeft();
    }

    private bool RightToGround()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Right
            && GameController.instance.IsInGround();
    }

    private bool RoofToLeft()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Roof
            && GameController.instance.IsInLeft();
    }

    private bool RoofToRight()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Roof
            && GameController.instance.IsInRight();
    }

    private bool RoofToGround()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Roof
            && GameController.instance.IsInGround();
    }

    private bool LeftToGround()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Left
            && GameController.instance.IsInGround();
    }

    private bool LeftToRoof()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Left
            && GameController.instance.IsInRoof();
    }

    private bool LeftToRight()
    {
        return GameController.instance.previousMagnetPosition == GameController.CheckDirection.Left
            && GameController.instance.IsInRight();
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
    
    public void SetBoostAbilityActive()
    {
        ChangeImage(boostAbility, boostActive);
    }
    public void SetBoostAbilityCooldown()
    {
        ChangeImage(boostAbility, boostCooldown);
    }

    public void SetJumpAbilityAvailable()
    {
        ChangeImage(jumpAbility, jumpAvailable);
    }

    public void SetJumpAbilityActive()
    {
        ChangeImage(jumpAbility, jumpActive);
    }
    public void SetJumpAbilityCooldown()
    {
        ChangeImage(jumpAbility, jumpCooldown);
    }

    public void SetFreezeAbilityAvailable()
    {
        ChangeImage(freezeAbility, freezeAvailable);
    }

    public void SetFreezeAbilityActive()
    {
        ChangeImage(freezeAbility, freezeActive);
    }
    public void SetFreezeAbilityCooldown()
    {
        ChangeImage(freezeAbility, freezeCooldown);
    }

    public void DeactivateElements()
    {
        mapBox.SetActive(false);
        mapKeys.SetActive(false);
        boostAbility.SetActive(false);
        jumpAbility.SetActive(false);
        freezeAbility.SetActive(false);
        freezeTxt.SetActive(false);
        jumpTxt.SetActive(false);
        boostTxt.SetActive(false);
    }

}
