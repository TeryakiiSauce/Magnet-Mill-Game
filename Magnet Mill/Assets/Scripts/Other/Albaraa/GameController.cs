using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public enum CheckDirection { Ground, Right, Left, Roof };
    public enum PowerAbilities { None, JumpAbility, FreezeAbility, BoostAbility };
    public string currentLevel;
    public GameObject cube;
    public Material checkPointOnMaterial;

    //[HideInInspector] public bool gameOver;
    [HideInInspector] public bool gamePaused;
    [HideInInspector] public float levelTimer;
    [HideInInspector] public int score;
    [HideInInspector] public int rollsCount;
    [HideInInspector] public int deathCount;
    [HideInInspector] public int abilitesUsedCount;
    [HideInInspector] public PowerAbilities currentAbility;
    [HideInInspector] public CheckDirection previousMagnetPosition;

    private bool isDead;
    private bool isLevelFinished;
    private CheckDirection currentMagnetPosition;
    private CheckDirection checkPointCurrentDirection;
    private Vector3 currentCheckPointPos;

    void Awake()
    {
        if (instance == null)       //checking if instance is null or not becuase we only need one instance of this class
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (AudioManager.instance != null)        //Checking if audiomanager is null, if yes it means that the scene not started from
        {                                       //main menu, then the audio will not play since its object is null
            if(!AudioManager.instance.IsPlaying("BackGroundMusic")) AudioManager.instance.Play("BackGroundMusic");  //play sound by its name that given in the main menu in audio manager
        }
        UserData.SetString(UserData.currentLevel, currentLevel);    //assign current level local storage variable with this scene
    }

    public void SetCheckPoint(string gridTag)
    {
        if (gridTag == "Ground" || gridTag == "GroundCorner")   //check the checkpoint tag
        {
            checkPointCurrentDirection = CheckDirection.Ground;
            currentCheckPointPos = new Vector3(Mathf.RoundToInt(cube.transform.position.x),
                cube.transform.position.y + 0.4f, Mathf.RoundToInt(cube.transform.position.z));     //save spawning position
        }
        else if(gridTag == "Right wall" || gridTag == "RightWallCorner")
        {
            checkPointCurrentDirection = CheckDirection.Right;
            currentCheckPointPos = new Vector3(cube.transform.position.x - 0.4f,
                Mathf.RoundToInt(cube.transform.position.y), Mathf.RoundToInt(cube.transform.position.z));
        }
        else if(gridTag == "Roof" || gridTag == "RoofCorner")
        {
            checkPointCurrentDirection = CheckDirection.Roof;
            currentCheckPointPos = new Vector3(Mathf.RoundToInt(cube.transform.position.x),
                cube.transform.position.y - 0.4f, Mathf.RoundToInt(cube.transform.position.z));
        }
        else if(gridTag == "Left wall" || gridTag == "leftWallCorner")
        {
            checkPointCurrentDirection = CheckDirection.Left;
            currentCheckPointPos = new Vector3(cube.transform.position.x + 0.4f,
                Mathf.RoundToInt(cube.transform.position.y), Mathf.RoundToInt(cube.transform.position.z));
        }
    }

    public void OutOfMap()     //this function will be called if the user is out of map (dead)
    {
        Rigidbody cubeBody = cube.GetComponent<Rigidbody>();
        if (checkPointCurrentDirection == CheckDirection.Ground)
        {
            InGround();     //change magnet direction to ground if the checkpoint is in the ground
        }
        else if(checkPointCurrentDirection == CheckDirection.Right)
        {
            InRight();        
        }
        else if(checkPointCurrentDirection == CheckDirection.Roof)
        {
            InRoof();
        }
        else if(checkPointCurrentDirection == CheckDirection.Left)
        {
            InLeft();
        }
        cubeBody.velocity = Vector3.zero;       //stop cube velocity to handle constant changing of magnet direction
        cube.transform.position = currentCheckPointPos;    //change cube position to checkpoint position
        isDead = false;     // switch isDead back to false since the player respawned
    }

    public void InGround()  //change magnet direction to ground
    {
        if (IsInGround()) return;
        previousMagnetPosition = currentMagnetPosition;
        currentMagnetPosition = CheckDirection.Ground;
        HUDController.instance.SetMapAngle();
        HUDController.instance.SetGroundKeysImg();
        HUDController.instance.rotateSquare = true;
        HUDController.instance.angleSet = false;
    }

    public void InRight()   //change magnet direction to right
    {
        if (IsInRight()) return;
        previousMagnetPosition = currentMagnetPosition;
        currentMagnetPosition = CheckDirection.Right;
        HUDController.instance.SetMapAngle();
        HUDController.instance.SetRightKeysImg();
        HUDController.instance.rotateSquare = true;
        HUDController.instance.angleSet = false;
    }

    public void InRoof()    //change magnet direction to roof
    {
        if (IsInRoof()) return;
        previousMagnetPosition = currentMagnetPosition;
        currentMagnetPosition = CheckDirection.Roof;
        HUDController.instance.SetMapAngle();
        HUDController.instance.SetRoofKeysImg();
        HUDController.instance.rotateSquare = true;
        HUDController.instance.angleSet = false;
    }

    public void InLeft()    //change magnet direction to left
    {
        if (IsInLeft()) return;
        previousMagnetPosition = currentMagnetPosition;
        currentMagnetPosition = CheckDirection.Left;
        HUDController.instance.SetMapAngle();
        HUDController.instance.SetLeftKeysImg();
        HUDController.instance.rotateSquare = true;
        HUDController.instance.angleSet = false;
    }

    public bool IsInRoof()
    {
        return currentMagnetPosition == CheckDirection.Roof;
    }

    public bool IsInGround()
    {
        return currentMagnetPosition == CheckDirection.Ground;
    }

    public bool IsInRight()
    {
        return currentMagnetPosition == CheckDirection.Right;
    }

    public bool IsInLeft()
    {
        return currentMagnetPosition == CheckDirection.Left;
    }

    public CheckDirection GetCurrentDirection()
    {
        return currentMagnetPosition;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool IsLevelFinshed()
    {
        return isLevelFinished;
    }

    public void PlayerDead()
    {
        isDead = true;
    }

    public void LevelFinished()
    {
        isLevelFinished = true;
        TimerController.instance.PauseTimer();
        HUDController.instance.DeactivateElements();
        if (currentLevel == "Level0")
        {
            UserData.SetBool(UserData.finishedTutorial, true);
        }
        else if(currentLevel == "Level1")
        {
            UserData.SetBool(UserData.finishedLevel1, true);
        }
        else if (currentLevel == "Level2")
        {
            UserData.SetBool(UserData.finishedLevel2, true);
        }
        else if (currentLevel == "Level3")
        {
            UserData.SetBool(UserData.finishedLevel3, true);
        }
        else if (currentLevel == "Level4")
        {
            UserData.SetBool(UserData.finishedLevel4, true);
        }

    }

    public Vector3 CubePosition()
    {
        return cube.transform.position;
    }
}
