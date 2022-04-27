using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [HideInInspector] public bool gameOver;
    [HideInInspector] public bool gamePaused;
    public enum PowerAbilities {None ,JumpAbility, FreezeAbility, BoostAbility};
    [HideInInspector] public PowerAbilities currentAbility;
    [HideInInspector] public int score;
    [HideInInspector] public int rollsCount;
    [HideInInspector] public float levelTimer;
    [HideInInspector] public int deathCount;
    [HideInInspector] public int abilitesUsedCount;
    //public bool isOutOffMap;
    public string currentLevel;
    public GameObject cube;
    public Material checkPointOnMaterial;
    [SerializeField] private Vector3 currentCheckPoint;
    public enum CheckDirection { Ground, Right, Left, Roof};
    [SerializeField] private CheckDirection checkPointCurrentDirection;
    public CheckDirection currentMagnetPosition;
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
        if (gridTag == "Ground")
        {
            checkPointCurrentDirection = CheckDirection.Ground;
            currentCheckPoint = new Vector3(Mathf.RoundToInt(cube.transform.position.x),
                cube.transform.position.y + 0.5f, Mathf.RoundToInt(cube.transform.position.z));
        }
        else if(gridTag == "Right wall")
        {
            checkPointCurrentDirection = CheckDirection.Right;
            currentCheckPoint = new Vector3(cube.transform.position.x - 1f,
                Mathf.RoundToInt(cube.transform.position.y), Mathf.RoundToInt(cube.transform.position.z));
        }
    }

    public void OutOffMap()
    {
        CubeController cubeCont = cube.GetComponent<CubeController>();
        Rigidbody cubeBody = cube.GetComponent<Rigidbody>();
        if(checkPointCurrentDirection == CheckDirection.Ground)
        {
            currentMagnetPosition = CheckDirection.Ground;
        }
        else if(checkPointCurrentDirection == CheckDirection.Right)
        {
            currentMagnetPosition = CheckDirection.Right;
        }
        cubeBody.velocity = Vector3.zero;
        cube.transform.position = currentCheckPoint;
        CubeController.outOfBounds = false;
    }

    public void InGround()
    {
        currentMagnetPosition = CheckDirection.Ground;
    }

    public void InRight()
    {
        currentMagnetPosition = CheckDirection.Right;
    }

    public void InRoof()
    {
        currentMagnetPosition = CheckDirection.Roof;
    }

    public void InLeft()
    {
        currentMagnetPosition = CheckDirection.Left;
    }
}
