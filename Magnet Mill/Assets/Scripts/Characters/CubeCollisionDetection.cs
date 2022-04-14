using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollisionDetection : MonoBehaviour
{
    private string enteredLayer, exitedLayer;
    //private bool moveUp = false;

    private void OnTriggerStay(Collider other)
    {
        if (exitedLayer == "Horizontal Grids" && enteredLayer == "Vertical Grids")
        {
            Debug.Log("Move up!");
            //moveUp = true;

            NewPlayerController.isMoving = true; // Interrupts the rotation of the cube

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                print("switch camera");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        enteredLayer = LayerMask.LayerToName(other.gameObject.layer);

        Debug.Log("Entered: " + enteredLayer);
    }

    private void OnTriggerExit(Collider other)
    {
        exitedLayer = LayerMask.LayerToName(other.gameObject.layer);

        Debug.Log("Left: " + enteredLayer);
    }
}
