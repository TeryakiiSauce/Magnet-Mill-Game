using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private GameObject[] quickCameras;
    private CinemachineVirtualCamera quickTopCam, quickBottomCam, quickRightCam, quickLeftCam; // For the virtual cameras to be assigned from the array "quickCameras"
    private CinemachineTransposer originalOffsetTopCam, originalOffsetBottomCam, originalOffsetRightCam, originalOffsetLeftCam; // To maintain original cameras' offsets
    private CinemachineTransposer newOffsetTopCam, newOffsetBottomCam, newOffsetRightCam, newOffsetLeftCam; // To store new cameras' offsets
    public GameObject mainFocus, groundFocus, topFocus, rightFocus, leftFocus; // For the quick virtual cameras aiming

    // Start is called before the first frame update
    void Start()
    {
        quickCameras = GameObject.FindGameObjectsWithTag("Quick Cam View");
        
        // Assign the virtual cameras found from the array to their respective variables. The order is based on Unity's Hierarchy panel (left panel by default).
        quickBottomCam = quickCameras[0].GetComponent<CinemachineVirtualCamera>();
        quickRightCam = quickCameras[1].GetComponent<CinemachineVirtualCamera>();
        quickTopCam = quickCameras[2].GetComponent<CinemachineVirtualCamera>();
        quickLeftCam = quickCameras[3].GetComponent<CinemachineVirtualCamera>();

        /* Testing
        Debug.Log(quickBottomCam);
        Debug.Log(quickRightCam);
        Debug.Log(quickTopCam);
        Debug.Log(quickLeftCam);
        */

        // Default LookAt value for the quick camera -> i.e. they should face the cube by default
        quickBottomCam.LookAt = mainFocus.transform;
        quickRightCam.LookAt = mainFocus.transform;
        quickLeftCam.LookAt = mainFocus.transform;
        quickTopCam.LookAt = mainFocus.transform;
        
        // Maintain original camera offset
        originalOffsetTopCam = quickTopCam.GetComponent<CinemachineTransposer>();
        originalOffsetBottomCam = quickBottomCam.GetComponent<CinemachineTransposer>();

        newOffsetTopCam = groundFocus.GetComponent<CinemachineTransposer>();
        newOffsetBottomCam = quickBottomCam.GetComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //quickTopCam.Priority = 12;

            print(newOffsetTopCam);

            quickBottomCam.LookAt = topFocus.transform;
            quickRightCam.LookAt = topFocus.transform;
            quickLeftCam.LookAt = topFocus.transform;
            quickTopCam.LookAt = topFocus.transform;
        }
        else
        {
            //quickTopCam.Priority = 10;

            quickBottomCam.LookAt = mainFocus.transform;
            quickRightCam.LookAt = mainFocus.transform;
            quickLeftCam.LookAt = mainFocus.transform;
            quickTopCam.LookAt = mainFocus.transform;
        }
    }
}