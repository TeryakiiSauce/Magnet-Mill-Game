using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int[] gridsX = null, gridsY = null, gridsZ = null;

    // To store the corners of the level
    private static Dictionary<string, Vector3> cornersDict = new Dictionary<string, Vector3>();



    private void Awake()
    {
        currentCubePosition = cubeStartingPosition;

        // Return if any of the values are 0 or less since I think it will cause issues.
        if (/*cubeStartingPosition.x <= 0 || cubeStartingPosition.y <= 0 || cubeStartingPosition.z <= 0 || */ totalGridsX <= 0 || totalGridsY <= 0 || totalGridsZ <= 0) return;

        gridsX = new int[totalGridsX];
        gridsY = new int[totalGridsY];
        gridsZ = new int[totalGridsZ];

        // Set corners vectors to dictionary (-1 >>> means "anything")
        cornersDict.Add(Corners.BottomLeftCorner, new Vector3(0, 0, -1));
        cornersDict.Add(Corners.BottomRightCorner, new Vector3(gridsX.Length - 1, 0, -1)); // gridsX.Length - 1 >>> Gets last index
        cornersDict.Add(Corners.TopLeftCorner, new Vector3(1, gridsY.Length - 1, -1));
        cornersDict.Add(Corners.TopRightCorner, new Vector3(gridsX.Length - 1, gridsY.Length - 1, -1));
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
        
    }



    /// <summary>
    /// Checks whether it is possible for the cube to continue rolling or not.
    /// </summary>
    public static void CheckHitWall()
    {
        // The Z axis should not be read that's why I did it this way
        if (currentCubePosition.x == cornersDict[Corners.BottomLeftCorner].x && currentCubePosition.y == cornersDict[Corners.BottomLeftCorner].y)
        {
            Debug.Log("Corner hit");
            NewPlayerController.hasHitWall = true;
        }
        else if (currentCubePosition.x == cornersDict[Corners.BottomRightCorner].x && currentCubePosition.y == cornersDict[Corners.BottomRightCorner].y)
        {
            Debug.Log("Corner hit");
            NewPlayerController.hasHitWall = true;
        }
        else if (currentCubePosition.x == cornersDict[Corners.TopLeftCorner].x && currentCubePosition.y == cornersDict[Corners.TopLeftCorner].y)
        {
            Debug.Log("Corner hit");
            NewPlayerController.hasHitWall = true;
        }
        else if (currentCubePosition.x == cornersDict[Corners.TopRightCorner].x && currentCubePosition.y == cornersDict[Corners.TopRightCorner].y)
        {
            Debug.Log("Corner hit");
            NewPlayerController.hasHitWall = true;
        }
    }
}
