using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CubeController : MonoBehaviour
{
    public int speed = 300;

    private Rigidbody cubeRigidBody;

    public static bool isMoving = false;
    public static bool flipinggravity = false;
    private static bool isJumping = false;
    public static bool outOfBounds = false;
    // Added public static getters so that they can be called from different scripts such as "CameraController.cs" and Ali's script for the HUD

    // Start is called before the first frame update
    void Start()
    {
        // Choose one depending on needs
        cubeRigidBody = GetComponent<Rigidbody>();
        //cubeRigidBody = GetComponentInChildren<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {


        if (isMoving || flipinggravity || outOfBounds) return; // to prevent rolling when we are in the middle of a roll or when clicking space

        //if statment to check which platform the cube is sitting on 
        if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Ground)//onGround)
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.left)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.right)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }
        else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Roof)
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.left)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.right)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }
        else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Right)
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.down)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.up)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }
        else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Left)
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.up)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.down)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }

        if (Input.GetKey(KeyCode.E))
        {
            isJumping = false;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            isJumping = true;
        }

        //if the user presses space 
        if (Input.GetKey(KeyCode.Space))
        {
            flipinggravity = true;
            //flip from ground to roof
            if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Ground)
            {
                GameController.instance.InRoof();
            }
            //flip from roof to ground 
            else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Roof)
            {
                GameController.instance.InGround();
            }
            //flip from right to left
            else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Right)
            {
                GameController.instance.InLeft();
            }
            //flip from left to right 
            else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Left)
            {
                GameController.instance.InRight();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {


        flipinggravity = false;
        //Check to see if the tag on the collider is equal to Enemy
        if (other.tag == "Right wall")
        {
            GameController.instance.InRight();
        }
        else if (other.tag == "Left wall")
        {
            GameController.instance.InLeft();
        }
        else if (other.tag == "Roof")
        {
            GameController.instance.InRoof();
        }
        else if (other.tag == "Ground")
        {
            GameController.instance.InGround();
        }
        else if (other.tag == "outOfBound")
        {
            outOfBounds = true;
        }
    }
    void FixedUpdate()
    {
        //if statment that changes the force(gravity) depending on where the cube is touching 
        if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Ground)
        {
            cubeRigidBody.AddForce(0, -9.81f, 0);
        }
        else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Roof)
        {
            cubeRigidBody.AddForce(0, 9.81f, 0);
        }
        else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Right)
        {
            cubeRigidBody.AddForce(9.81f, 0, 0);
        }
        else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Left)
        {
            cubeRigidBody.AddForce(-9.81f, 0, 0);
        }
    }

    IEnumerator Roll(Vector3 direction)
    {

        //disabling movment for the user 
        isMoving = true;
        //seting the angle of rotation 
        float remainingAngle = 90;
        //default values for the user 
        Vector3 rotationCenter = transform.localPosition + direction / 2 + Vector3.down / 2; // direction of the rotation
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction); // compute the rotation access based on the direction and y axis

        if (isJumping)
        {
            if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Roof)
            {
                rotationCenter = transform.localPosition + direction / 0.85f + Vector3.up / 0.85f; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.down, direction); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Right)
            {
                rotationCenter = transform.localPosition + direction / 0.85f + Vector3.right / 0.85f; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.left, direction); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Left)
            {
                rotationCenter = transform.localPosition + direction / 0.85f + Vector3.left / 0.85f; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.right, direction); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Ground)
            {
                rotationCenter = transform.localPosition + direction / 0.85f + Vector3.down / 0.85f; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.up, direction); // compute the rotation access based on the direction and y axis
            }
        }
        else
        {
            //if statment that changes the axes and the rotation depneing on which wall is the cube touching 
            if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Roof)
            {
                rotationCenter = transform.localPosition + direction / 2 + Vector3.up / 2; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.down, direction); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Right)
            {
                rotationCenter = transform.localPosition + direction / 2 + Vector3.right / 2; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.left, direction); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Left)
            {
                rotationCenter = transform.localPosition + direction / 2 + Vector3.left / 2; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.right, direction); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Ground)
            {
                rotationCenter = transform.localPosition + direction / 2 + Vector3.down / 2; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.up, direction); // compute the rotation access based on the direction and y axis
            }
        }

        while (remainingAngle > 0)
        {

            // make sure rotation angle will not be greater than remaining angle
            float rotatingAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            // rotate the cube around its edge
            transform.RotateAround(rotationCenter, rotationAxis, rotatingAngle);
            remainingAngle -= rotatingAngle;
            yield return null;
        }
        cubeRigidBody.velocity = Vector3.zero;
        cubeRigidBody.angularVelocity = Vector3.zero;

        if (remainingAngle < 5)
        {
            snapToGrid();
            isMoving = false;
        }

        //ebnabling the use to move again 

    }

    //method to snap the cube to the grid
    private void snapToGrid()
    {
        //checks if the cube is on the y axis walls or the x axis walls and snaps accordingly 
        if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Ground
            || GameController.instance.currentMagnetPosition == GameController.CheckDirection.Roof)
        {
            var position = new Vector3(
            Mathf.RoundToInt(transform.position.x),
            transform.position.y,
            Mathf.RoundToInt(transform.position.z)
            );
            this.transform.position = position;
        }

        if (GameController.instance.currentMagnetPosition == GameController.CheckDirection.Right
            || GameController.instance.currentMagnetPosition == GameController.CheckDirection.Left)
        {
            var position = new Vector3(
            transform.position.x,
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.z)
            );
            this.transform.position = position;
        }
    }
}
