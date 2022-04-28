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
    [HideInInspector] public bool gameOver;
    [HideInInspector] public bool gamePaused;
    [HideInInspector] public PowerAbilities currentAbility;
    [HideInInspector] public int score;
    [HideInInspector] public int rollsCount;
    [HideInInspector] public float levelTimer;
    [HideInInspector] public int deathCount;
    [HideInInspector] public int abilitesUsedCount;
    [HideInInspector] public CheckDirection currentMagnetPosition;
    [HideInInspector] public CheckDirection previousMagnetPosition;
    private Vector3 currentCheckPoint;
    private CheckDirection checkPointCurrentDirection;
    void Awake()
    {
        if (instance == null)
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
        UserData.SetString(UserData.currentLevel, currentLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCheckPoint(string gridTag)
    {
        if (gridTag == "Ground" || gridTag == "GroundCorner")
        {
            checkPointCurrentDirection = CheckDirection.Ground;
            currentCheckPoint = new Vector3(Mathf.RoundToInt(cube.transform.position.x),
                cube.transform.position.y + 0.5f, Mathf.RoundToInt(cube.transform.position.z));
        }
        else if(gridTag == "Right wall" || gridTag == "RightWallCorner")
        {
            checkPointCurrentDirection = CheckDirection.Right;
            currentCheckPoint = new Vector3(cube.transform.position.x - 1f,
                Mathf.RoundToInt(cube.transform.position.y), Mathf.RoundToInt(cube.transform.position.z));
        }
    }

    public void OutOffMap()
    {
        Rigidbody cubeBody = cube.GetComponent<Rigidbody>();
        if (checkPointCurrentDirection == CheckDirection.Ground)
        {
            InGround();
        }
        else if(checkPointCurrentDirection == CheckDirection.Right)
        {
            InRight();
        }
        cubeBody.velocity = Vector3.zero;
        cube.transform.position = currentCheckPoint;
        CubeController.outOfBounds = false;
    }

    public void InGround()
    {
        if (currentMagnetPosition == CheckDirection.Ground) return;
        previousMagnetPosition = currentMagnetPosition;
        currentMagnetPosition = CheckDirection.Ground;
        MapController.instance.SetGroundKeysImg();
        MapController.instance.rotateSquare = true;
    }

    public void InRight()
    {
        if (currentMagnetPosition == CheckDirection.Right) return;
        previousMagnetPosition = currentMagnetPosition;
        currentMagnetPosition = CheckDirection.Right;
        MapController.instance.SetRightKeysImg();
        MapController.instance.rotateSquare = true;
    }

    public void InRoof()
    {
        if (currentMagnetPosition == CheckDirection.Roof) return;
        previousMagnetPosition = currentMagnetPosition;
        currentMagnetPosition = CheckDirection.Roof;
        MapController.instance.SetRoofKeysImg();
        MapController.instance.rotateSquare = true;
    }

    public void InLeft()
    {
        if (currentMagnetPosition == CheckDirection.Left) return;
        previousMagnetPosition = currentMagnetPosition;
        currentMagnetPosition = CheckDirection.Left;
        MapController.instance.SetLeftKeysImg();
        MapController.instance.rotateSquare = true;
    }
}
