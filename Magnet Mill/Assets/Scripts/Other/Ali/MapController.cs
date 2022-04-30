using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapController : MonoBehaviour
{
    public static MapController instance;

    public GameObject mapBox;
    public GameObject mapKeys;
    public RectTransform rect;
    [HideInInspector] public bool rotateSquare;
    private Vector3 tempQ;
    private float rotationEndAngle = 90;
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
        if (RightToGround() || LeftToGround()||RoofToGround()) {
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
       

        tempQ = rect.eulerAngles;

        if (tempQ.z <= rotationEndAngle)
        {
            tempQ.z += rotationSpeed * Time.deltaTime;
            rect.eulerAngles = tempQ;

            if (Mathf.Approximately(tempQ.z, rotationEndAngle) || tempQ.z == rotationEndAngle || tempQ.z >= rotationEndAngle)
            {
                rotateSquare = false;
                tempQ.z = rotationEndAngle; // set the exact angle without any fractions
            }

        }
        else if (tempQ.z > rotationEndAngle)
        {
            tempQ.z -= rotationSpeed * Time.deltaTime;
            rect.eulerAngles = tempQ;

            if (Mathf.Approximately(tempQ.z, rotationEndAngle) || tempQ.z == rotationEndAngle || tempQ.z <= rotationEndAngle)
            {
                rotateSquare = false;
                tempQ.z = rotationEndAngle; // set the exact angle without any fractions
            }
        }
        else
        {
            rotateSquare = false;
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

    public void SetGroundKeysImg()
    {

    }

    public void SetRightKeysImg()
    {

    }

    public void SetRoofKeysImg()
    {

    }

    public void SetLeftKeysImg()
    {

    }
}
