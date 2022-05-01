using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera groundCam;
    public CinemachineVirtualCamera rightCam;
    public CinemachineVirtualCamera topCam;
    public CinemachineVirtualCamera leftCam;
    public CinemachineVirtualCamera flippingCam;

    public CinemachineVirtualCamera lookUpCam;
    public CinemachineVirtualCamera lookLeftCam;
    public CinemachineVirtualCamera lookDownCam;
    public CinemachineVirtualCamera lookRightCam;

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
    }

    // Update is called once per frame
    void Update()
    {
        // For debugging only!
        DebugCubePositionAndFlipStatus(false);

        // Idk why, but it felt more meaningful to check for the quick camera input before the main camera transition
        QuickCameraTransitionCheck();

        MainCameraTransition();
    }

    // Displays the Cube's current position (in terms of On Ground, On Roof, etc) and the "gravity" flip status
    private void DebugCubePositionAndFlipStatus(bool isTurnedOn)
    {
        if (isTurnedOn)
        {
            if (GameController.instance.IsInGround())
            {
                Debug.Log("on ground");
                Debug.Log($"Flipping status: {CubeController.flipinggravity}");
            }
            else if (GameController.instance.IsInLeft())
            {
                Debug.Log("on left wall");
                Debug.Log($"Flipping status: {CubeController.flipinggravity}");
            }
            else if (GameController.instance.IsInRight())
            {
                Debug.Log("on right wall");
                Debug.Log($"Flipping status: {CubeController.flipinggravity}");
            }
            else if (GameController.instance.IsInRoof())
            {
                Debug.Log("on roof");
                Debug.Log($"Flipping status: {CubeController.flipinggravity}");
            }
        }
    }

    private void MainCameraTransition()
    {
        if (GameController.instance.IsInGround())
        {
            groundCam.Priority = 1;
            rightCam.Priority = 0;
            leftCam.Priority = 0;
            topCam.Priority = 0;
        }
        else if (GameController.instance.IsInRight())
        {
            groundCam.Priority = 0;
            rightCam.Priority = 1;
            leftCam.Priority = 0;
            topCam.Priority = 0;
        }
        else if (GameController.instance.IsInLeft())
        {
            groundCam.Priority = 0;
            rightCam.Priority = 0;
            leftCam.Priority = 1;
            topCam.Priority = 0;
        }
        else if (GameController.instance.IsInRoof())
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
        if (GameController.instance.IsInGround())
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Tab))
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
        else if (GameController.instance.IsInRight())
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Tab))
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
        else if (GameController.instance.IsInRoof())
        {
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Tab))
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
        else if (GameController.instance.IsInLeft())
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Tab))
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