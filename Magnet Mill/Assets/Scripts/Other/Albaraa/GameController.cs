using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public enum CheckDirection { Ground, Right, Left, Roof };
    public string currentLevel;
    public int levelMaxScore;
    public GameObject cube;
    public Material checkPointOnMaterial;

    //[HideInInspector] public bool gameOver;
    //[HideInInspector] public bool gamePaused;
    [HideInInspector] public int score;
    [HideInInspector] public int rollsCount;
    [HideInInspector] public int deathCount;
    [HideInInspector] public int abilitesUsedCount;
    [HideInInspector] public CheckDirection previousMagnetPosition;

    private bool isDead;
    private bool isLevelFinished;
    private CheckDirection currentMagnetPosition;
    private CheckDirection checkPointCurrentDirection;
    private Vector3 currentCheckPointPos;
    private Rigidbody cubeBody;

    void Awake()
    {
        if (instance == null)       //checking if instance is null or not becuase we only need one instance of this class
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
            return;
        }
    }
    void Start()
    {
        cubeBody = cube.GetComponent<Rigidbody>();
        if (AudioManager.instance != null)        //Checking if audiomanager is null, if yes it means that the scene not started from
        {                                       //main menu, then the audio will not play since its object is null
            if (currentLevel == "Level0")
            {
                AudioManager.instance.Stop("MainMenuTheme");
            }
            else if (currentLevel == "Level1")
            {
                AudioManager.instance.Stop("MainMenuTheme");
                AudioManager.instance.Stop("Level0Theme");
            }
            else if (currentLevel == "Level2")
            {
                AudioManager.instance.Stop("MainMenuTheme");
                AudioManager.instance.Stop("Level1Theme");
            }
            else if (currentLevel == "Level3")
            {
                AudioManager.instance.Stop("MainMenuTheme");
                AudioManager.instance.Stop("Level2Theme");
            }
            else if (currentLevel == "Level4")
            {
                AudioManager.instance.Stop("MainMenuTheme");
                AudioManager.instance.Stop("Level3Theme");
            }
            if (!AudioManager.instance.IsPlaying(currentLevel + "Theme")) AudioManager.instance.Play(currentLevel + "Theme");  //play current level theme sound
        }
        UserData.SetString(UserData.currentLevel, currentLevel);    //assign current level local storage variable with this scene
    }

    public void SetCheckPoint(GameObject grid)
    {
        if (grid.tag == "Ground" || grid.tag == "GroundCorner")   //check the checkpoint tag
        {
            checkPointCurrentDirection = CheckDirection.Ground;
            currentCheckPointPos = new Vector3(Mathf.RoundToInt(grid.transform.position.x),
                grid.transform.position.y + 0.55f, Mathf.RoundToInt(grid.transform.position.z));     //save spawning position
        }
        else if (grid.tag == "Right wall" || grid.tag == "RightWallCorner")
        {
            checkPointCurrentDirection = CheckDirection.Right;
            currentCheckPointPos = new Vector3(grid.transform.position.x - 0.55f,
                Mathf.RoundToInt(grid.transform.position.y), Mathf.RoundToInt(grid.transform.position.z));
        }
        else if (grid.tag == "Roof" || grid.tag == "RoofCorner")
        {
            checkPointCurrentDirection = CheckDirection.Roof;
            currentCheckPointPos = new Vector3(Mathf.RoundToInt(grid.transform.position.x),
                grid.transform.position.y - 0.55f, Mathf.RoundToInt(grid.transform.position.z));
        }
        else if (grid.tag == "Left wall" || grid.tag == "leftWallCorner")
        {
            checkPointCurrentDirection = CheckDirection.Left;
            currentCheckPointPos = new Vector3(grid.transform.position.x + 0.55f,
                Mathf.RoundToInt(grid.transform.position.y), Mathf.RoundToInt(grid.transform.position.z));
        }
    }

    public void OutOfMap()     //this function will be called if the user is out of map (dead)
    {
        UserData.IncrementInt(UserData.numOfDeaths);
        deathCount++;
        cubeBody.velocity = Vector3.zero;       //stop cube velocity to handle instant changing of magnet direction
        if (checkPointCurrentDirection == CheckDirection.Ground)
        {
            InGround();     //change magnet direction to ground if the checkpoint is in the ground
        }
        else if (checkPointCurrentDirection == CheckDirection.Right)
        {
            InRight();
        }
        else if (checkPointCurrentDirection == CheckDirection.Roof)
        {
            InRoof();
        }
        else if (checkPointCurrentDirection == CheckDirection.Left)
        {
            InLeft();
        }
        cube.transform.position = currentCheckPointPos;    //change cube position to checkpoint position
        CubeController.flipinggravity = false;
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
        if (currentLevel != "Level0")
        {
            UserData.IncrementInt(UserData.numOfLevelsFinished);
            UserData.IncrementInt(UserData.numOfTotalScore, score);
        }
        else
        {
            UserData.SetBool(UserData.finishedTutorial, true);
        }
        UserData.IncrementFloat(UserData.totalTimePlayed, TimerController.instance.GetSceneTimer());
        TimerController.instance.PauseTimer();
        HUDController.instance.DeactivateElements();
    }

    public Vector3 CubePosition()
    {
        return cube.transform.position;
    }
}
