using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapController : MonoBehaviour
{
    public static MapController instance;

    public GameObject mapBox;
    public GameObject mapKeys;
    public Vector3 tempQ;
    public float endZAngle = 10;
    public int rotationSpeed = 100;
    [HideInInspector] public bool rotateSquare;
    public RectTransform rect;

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
        tempQ = rect.eulerAngles;

        if (tempQ.z < endZAngle && !Mathf.Approximately(tempQ.z,endZAngle))
        {
            tempQ.z += rotationSpeed * Time.deltaTime;
            rect.eulerAngles = tempQ;

        }
        else if(tempQ.z > endZAngle) 
        {
            tempQ.z = endZAngle;
            rect.eulerAngles = tempQ;

        }
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

}
