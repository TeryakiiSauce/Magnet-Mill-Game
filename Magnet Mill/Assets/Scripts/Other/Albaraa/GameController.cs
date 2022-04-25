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
    [SerializeField] private enum CheckPointDirection { Ground, Right, Left, Roof};
    [SerializeField] private CheckPointDirection checkPointCurrentDirection;
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
            checkPointCurrentDirection = CheckPointDirection.Ground;
            currentCheckPoint = new Vector3(cube.transform.position.x,
                cube.transform.position.y + 0.5f, cube.transform.position.z);
        }
        else if(gridTag == "Right wall")
        {
            checkPointCurrentDirection = CheckPointDirection.Right;
            currentCheckPoint = new Vector3(cube.transform.position.x - 1f,
                cube.transform.position.y, cube.transform.position.z);
        }
    }

    public void OutOffMap()
    {
        CubeController cubeCont = cube.GetComponent<CubeController>();
        Rigidbody cubeBody = cube.GetComponent<Rigidbody>();
        if(checkPointCurrentDirection == CheckPointDirection.Ground)
        {
            cubeCont.SetGroundDirection();
        }
        else if(checkPointCurrentDirection == CheckPointDirection.Right)
        {
            cubeCont.SetRightDirection();
        }
        cubeBody.velocity = Vector3.zero;
        cube.transform.position = currentCheckPoint;
        CubeController.outOfBounds = false;
    }
}
