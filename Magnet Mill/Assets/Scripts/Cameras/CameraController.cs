using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    /*
     * private GameObject[] quickCameras;
    private CinemachineVirtualCamera quickTopCam, quickBottomCam, quickRightCam, quickLeftCam; // For the virtual cameras to be assigned from the array "quickCameras"
    private CinemachineTransposer originalOffsetTopCam, originalOffsetBottomCam, originalOffsetRightCam, originalOffsetLeftCam; // To maintain original cameras' offsets
    private CinemachineTransposer newOffsetTopCam, newOffsetBottomCam, newOffsetRightCam, newOffsetLeftCam; // To store new cameras' offsets
    */

    public CinemachineVirtualCamera groundCam;
    public CinemachineVirtualCamera rightCam;
    public CinemachineVirtualCamera topCam;
    public CinemachineVirtualCamera leftCam;
    public CinemachineVirtualCamera flippingCam;

    public CinemachineVirtualCamera lookUpCam;
    public CinemachineVirtualCamera lookLeftCam;
    public CinemachineVirtualCamera lookDownCam;
    public CinemachineVirtualCamera lookRightCam;

    //public GameObject mainFocusPoint, groundFocusPoint, topFocusPoint, rightFocusPoint, leftFocusPoint; // For the quick virtual cameras aiming

    //public GameObject mainFocus, groundFocus, topFocus, rightFocus, leftFocus; // For the quick virtual cameras aiming

    // Start is called before the first frame update
    void Start()
    {
        // Main Cameras
        groundCam.Priority = 1;
        rightCam.Priority = 0;
        topCam.Priority = 0;
        leftCam.Priority = 0;
        flippingCam.Priority = 0;

        // Quick Cameras
        lookUpCam.Priority = 0;
        lookLeftCam.Priority = 0;
        lookDownCam.Priority = 0;
        lookRightCam.Priority = 0;

        /*quickCameras = GameObject.FindGameObjectsWithTag("Quick Cam View");
        
        // Assign the virtual cameras found from the array to their respective variables. The order is based on Unity's Hierarchy panel (left panel by default).
        quickBottomCam = quickCameras[0].GetComponent<CinemachineVirtualCamera>();
        quickRightCam = quickCameras[1].GetComponent<CinemachineVirtualCamera>();
        quickTopCam = quickCameras[2].GetComponent<CinemachineVirtualCamera>();
        quickLeftCam = quickCameras[3].GetComponent<CinemachineVirtualCamera>();

        Testing
        Debug.Log(quickBottomCam);
        Debug.Log(quickRightCam);
        Debug.Log(quickTopCam);
        Debug.Log(quickLeftCam);

        // Default LookAt value for the quick camera -> i.e. they should face the cube by default
        quickBottomCam.LookAt = mainFocus.transform;
        quickRightCam.LookAt = mainFocus.transform;
        quickLeftCam.LookAt = mainFocus.transform;
        quickTopCam.LookAt = mainFocus.transform;
        
        // Maintain original camera offset
        originalOffsetTopCam = quickTopCam.GetComponent<CinemachineTransposer>();
        originalOffsetBottomCam = quickBottomCam.GetComponent<CinemachineTransposer>();

        newOffsetTopCam = groundFocus.GetComponent<CinemachineTransposer>();
        newOffsetBottomCam = quickBottomCam.GetComponent<CinemachineTransposer>();*/

        //quickTopCam.LookAt = mainFocusPoint.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // For debugging only!
        DebugCubePositionAndFlipStatus(true);

        // Idk why, but it felt more meaningful to check for the quick camera input before the main camera transition
        QuickCameraTransitionCheck();

        // TODO: Add something like a ray cast here. This is to show *where* the cube will be if the player hits the space bar (i.e. flips "gravity")
        //
        // ---- INSERT CODE HERE ----
        //
        // END TODO

        MainCameraTransition();
    }

    // Displays the Cube's current position (in terms of On Ground, On Roof, etc) and the "gravity" flip status
    private void DebugCubePositionAndFlipStatus(bool isTurnedOn)
    {
        if (isTurnedOn)
        {
            if (CubeController.IsOnGround())
            {
                Debug.Log("on ground");
                Debug.Log($"Flipping status: {CubeController.IsGravityFlipping()}");
            }
            else if (CubeController.IsOnLeftWall())
            {
                Debug.Log("on left wall");
                Debug.Log($"Flipping status: {CubeController.IsGravityFlipping()}");
            }
            else if (CubeController.IsOnRightWall())
            {
                Debug.Log("on right wall");
                Debug.Log($"Flipping status: {CubeController.IsGravityFlipping()}");
            }
            else if (CubeController.IsOnRoof())
            {
                Debug.Log("on roof");
                Debug.Log($"Flipping status: {CubeController.IsGravityFlipping()}");
            }
        }
    }

    private void MainCameraTransition()
    {
        if (CubeController.IsOnGround())
        {
            groundCam.Priority = 1;
            rightCam.Priority = 0;
            leftCam.Priority = 0;
            topCam.Priority = 0;
        }
        else if (CubeController.IsOnRightWall())
        {
            groundCam.Priority = 0;
            rightCam.Priority = 1;
            leftCam.Priority = 0;
            topCam.Priority = 0;
        }
        else if (CubeController.IsOnLeftWall())
        {
            groundCam.Priority = 0;
            rightCam.Priority = 0;
            leftCam.Priority = 1;
            topCam.Priority = 0;
        }
        else if (CubeController.IsOnRoof())
        {
            groundCam.Priority = 0;
            rightCam.Priority = 0;
            leftCam.Priority = 0;
            topCam.Priority = 1;
        }
    }

    private void QuickCameraTransitionCheck()
    {

        // Check for [UP] arrow key when On Ground
        if (CubeController.IsOnGround())
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                lookUpCam.Priority = 1;
                groundCam.Priority = 0;
            }
            else
            {
                lookUpCam.Priority = 0;
                groundCam.Priority = 1;
            }
        }

        // Check for [LEFT] arrow key when On Right Wall
        else if (CubeController.IsOnRightWall())
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                lookLeftCam.Priority = 1;
                rightCam.Priority = 0;
            }
            else
            {
                lookLeftCam.Priority = 0;
                rightCam.Priority = 1;
            }
        }

        // Check for [DOWN] arrow key when On Roof
        else if (CubeController.IsOnRoof())
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                lookDownCam.Priority = 1;
                topCam.Priority = 0;
            }
            else
            {
                lookDownCam.Priority = 0;
                topCam.Priority = 1;
            }
        }

        // Check for [RIGHT] arrow key when On Left Wall
        else if (CubeController.IsOnLeftWall())
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                lookRightCam.Priority = 1;
                leftCam.Priority = 0;
            }
            else
            {
                lookRightCam.Priority = 0;
                leftCam.Priority = 1;
            }
        }
    }
}