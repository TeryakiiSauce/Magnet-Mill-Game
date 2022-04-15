using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// This class is just used to get the string values instead of typing them each time & to reduce logical errors
public static class Corners
{
    public static string BottomLeftCorner = "BottomLeftCorner";
    public static string BottomRightCorner = "BottomRightCorner";
    public static string TopLeftCorner = "TopLeftCorner";
    public static string TopRightCorner = "TopRightCorner";
}

public class GridSystem : MonoBehaviour
{
    // Note: Manually count the number of grids and enter them in the inspector.

    public static Vector3 currentCubePosition = new Vector3(0, 0, 0);

    public Vector3 cubeStartingPosition = new Vector3(0, 0, 0); // This variable will be used in "NewPlayerController.cs" in order to update the values whenever the cube is moved.
    public int totalGridsX = 2, totalGridsY = 2, totalGridsZ = 2; // Default Values

    // To create the "invisible" grid.
    public static int[] gridsX = null, gridsY = null, gridsZ = null;

    // To store the corners of the level
    private static Dictionary<string, Vector3> cornersDict = new Dictionary<string, Vector3>();

    public static bool isHorizontal = true; // Checks if player is on ground/ roof (horizontal) otherwise it means that the player is on left/right side (vertical)

    public CinemachineVirtualCamera groundCam;
    public CinemachineVirtualCamera leftCam;

    public static CinemachineVirtualCamera gCam;
    public static CinemachineVirtualCamera lCam;


    private void Awake()
    {
        currentCubePosition = cubeStartingPosition;

        // Return if any of the values are 0 or less since I think it will cause issues.
        if (totalGridsX <= 0 || totalGridsY <= 0 || totalGridsZ <= 0) return;

        gridsX = new int[totalGridsX];
        gridsY = new int[totalGridsY];
        gridsZ = new int[totalGridsZ];

        // Set corners vectors to dictionary (-1 >>> means "anything")
        cornersDict.Add(Corners.BottomLeftCorner, new Vector3(0, 0, -1));
        cornersDict.Add(Corners.BottomRightCorner, new Vector3(gridsX.Length - 1, 0, -1)); // gridsX.Length - 1 >>> Gets last index
        cornersDict.Add(Corners.TopLeftCorner, new Vector3(1, gridsY.Length - 1, -1));
        cornersDict.Add(Corners.TopRightCorner, new Vector3(gridsX.Length - 1, gridsY.Length - 1, -1));

        gCam = groundCam;
        lCam = leftCam;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Corners.BottomLeftCorner + ": " + cornersDict[Corners.BottomLeftCorner]);
        Debug.Log(Corners.BottomRightCorner + ": " + cornersDict[Corners.BottomRightCorner]);
        Debug.Log(Corners.TopLeftCorner + ": " + cornersDict[Corners.TopLeftCorner]);
        Debug.Log(Corners.TopRightCorner + ": " + cornersDict[Corners.TopRightCorner]);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current position: " + currentCubePosition);
    }



    /// <summary>
    /// Checks whether it is possible for the cube to continue rolling or not.
    /// <para>When wall is hit, the "TAB" key becomes available.</para>
    /// </summary>
    public static void CheckHitWall()
    {
        // The Z axis should not be read that's why I did it this way

        // Bottom Left Corner
        if (currentCubePosition.x == cornersDict[Corners.BottomLeftCorner].x && currentCubePosition.y == cornersDict[Corners.BottomLeftCorner].y)
        {
            Debug.Log("Corner hit");
            NewPlayerController.hasHitWall = true;

            // Checks if "Tab" is pressed otherwise let the cube move as it was.
            if (Input.GetKeyDown(KeyCode.Tab) && isHorizontal)
            {
                Debug.Log("Tab key has been pressed, switching to 'Vertical' mode.");
                isHorizontal = false;

                gCam.Priority = 0;
                lCam.Priority = 1;
            }

            else if (Input.GetKeyDown(KeyCode.Tab) && !isHorizontal)
            {
                Debug.Log("Tab key has been pressed, switching to 'Horizontal' mode.");
                isHorizontal = true;

                gCam.Priority = 1;
                lCam.Priority = 0;
            }

            // Cube continues as it was before
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                NewPlayerController.hasHitWall = false;
            }
        }

        // Bottom Right Corner
        else if (currentCubePosition.x == cornersDict[Corners.BottomRightCorner].x && currentCubePosition.y == cornersDict[Corners.BottomRightCorner].y)
        {
            Debug.Log("Corner hit");
            NewPlayerController.hasHitWall = true;

            // Checks if "Tab" is pressed otherwise let the cube move as it was.
            if (Input.GetKeyDown(KeyCode.Tab) && isHorizontal)
            {
                Debug.Log("Tab key has been pressed, switching to 'Vertical' mode.");
                isHorizontal = false;
            }

            else if (Input.GetKeyDown(KeyCode.Tab) && !isHorizontal)
            {
                Debug.Log("Tab key has been pressed, switching to 'Horizontal' mode.");
                isHorizontal = true;
            }

            // Cube continues as it was before
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                NewPlayerController.hasHitWall = false;
            }
        }

        // Top Left Corner
        else if (currentCubePosition.x == cornersDict[Corners.TopLeftCorner].x && currentCubePosition.y == cornersDict[Corners.TopLeftCorner].y)
        {
            Debug.Log("Corner hit");
            NewPlayerController.hasHitWall = true;

            // Checks if "Tab" is pressed otherwise let the cube move as it was.
            if (Input.GetKeyDown(KeyCode.Tab) && isHorizontal)
            {
                Debug.Log("Tab key has been pressed, switching to 'Vertical' mode.");
                isHorizontal = false;
            }

            else if (Input.GetKeyDown(KeyCode.Tab) && !isHorizontal)
            {
                Debug.Log("Tab key has been pressed, switching to 'Horizontal' mode.");
                isHorizontal = true;
            }

            // Cube continues as it was before
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                NewPlayerController.hasHitWall = false;
            }
        }

        // Top Right Corner
        else if (currentCubePosition.x == cornersDict[Corners.TopRightCorner].x && currentCubePosition.y == cornersDict[Corners.TopRightCorner].y)
        {
            Debug.Log("Corner hit");
            NewPlayerController.hasHitWall = true;

            // Checks if "Tab" is pressed otherwise let the cube move as it was.
            if (Input.GetKeyDown(KeyCode.Tab) && isHorizontal)
            {
                Debug.Log("Tab key has been pressed, switching to 'Vertical' mode.");
                isHorizontal = false;
            }

            else if (Input.GetKeyDown(KeyCode.Tab) && !isHorizontal)
            {
                Debug.Log("Tab key has been pressed, switching to 'Horizontal' mode.");
                isHorizontal = true;
            }

            // Cube continues as it was before
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                NewPlayerController.hasHitWall = false;
            }
        }
    }
}
