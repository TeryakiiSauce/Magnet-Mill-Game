using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CubeController : MonoBehaviour
{
    public int speed = 300;
    private Rigidbody cubeRigidBody;
    public bool normalMovement = true;
    public static bool isTouchingGround = true;
    public static bool isMoving = false;
    public static bool checkmovement = false;
    public static bool flipinggravity = false;
    //public static bool outOfBounds = false;
    private Vector3 rotationCenter;
    private Vector3 rotationAxis;
    public float jumpHeight = 0.8f;
    public float jumpLenght = 1.2f;
    private bool onCorner = false;
    float remainingAngle;
    private Time timer;
    OutOffBoundHandler OTBhandler;
    // Added public static getters so that they can be called from different scripts such as "CameraController.cs" and Ali's script for the HUD

    // Start is called before the first frame update
    void Start()
    {
        OTBhandler = GetComponent<OutOffBoundHandler>();
        // Choose one depending on needs
        cubeRigidBody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        OTBhandler.setTimer();

        if (isMoving || flipinggravity || !isTouchingGround ||GameController.instance.IsDead() || GameController.instance.IsLevelFinshed()) return; // to prevent rolling when we are in the middle of a roll or when clicking space
        userInput();
        checkSpeedAbility();

    }

    //fixed update is used to controll the constant force of gravity   
    void FixedUpdate()
    { 
        changeGravity();
        checkMovement();
    }


    //a method that checks the tag of the touched wall and updates the code accordingly 
    private void OnTriggerEnter(Collider other)
    {

       

        cubeRigidBody.velocity = Vector3.zero;
        cubeRigidBody.angularVelocity = Vector3.zero;
        
        flipinggravity = false;
        //Check to see if the tag on the collider is equal to Enemy
        if (other.tag == "Right wall")
        {
            GameController.instance.InRight();
            onCorner = false;
        }
        else if (other.tag == "Left wall")
        {
            GameController.instance.InLeft();
            onCorner = false;
        }
        else if (other.tag == "Roof")
        {
            GameController.instance.InRoof();
            onCorner = false;
        }
        else if (other.tag == "Ground")
        {
            GameController.instance.InGround();
            onCorner = false;
        }

        else if (other.tag == "RightWallCorner")
        {
            GameController.instance.InRight();
            onCorner = true;
        }
        else if (other.tag == "leftWallCorner")
        {
            GameController.instance.InLeft();
            onCorner = true;
        }
        else if (other.tag == "RoofCorner")
        {
            GameController.instance.InRoof();
            onCorner = true;
        }
        else if (other.tag == "GroundCorner")
        {
            GameController.instance.InGround();
            onCorner = true;
        }
        else if (other.tag == "outOfBound")
        {
            if (checkmovement)
            {

            }
            else 
            {
                GameController.instance.PlayerDead();
            }
        }
    }


    //method that calls different methods depending on what the user inputs on his keyboard
    private void userInput() 
    {
        
        //if statment to check which platform the cube is sitting on 
        if (GameController.instance.IsInGround())//onGround
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.left)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.right)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }
        else if (GameController.instance.IsInRoof())//on roof
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.left)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.right)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }
        else if (GameController.instance.IsInRight())//on right wall
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.down)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.up)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }
        else if (GameController.instance.IsInLeft())//on left wall
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.up)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.down)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }

        //if the user presses space 
        if (Input.GetKey(KeyCode.Space)) flipCube();
    }


    //a method that is used to move and rotate the cube on all 4 surfaces 
    IEnumerator Roll(Vector3 direction)
    {
        //disabling movment for the user 
        isMoving = true;
        remainingAngle = 90;
        //setting the roatation center and angle depending on what wall the cube is on 
        setRotation(direction);
        //moving the cube

        while (remainingAngle > 0)
        {
            // make sure rotation angle will not be greater than remaining angle
            float rotatingAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            // rotate the cube around its edge
            transform.RotateAround(rotationCenter, rotationAxis, rotatingAngle);
            remainingAngle -= rotatingAngle;
            yield return null; 
        }
        
        //snaping the angle to the grid and enabling it to move when the remaining angle of thew rotation is less then 5
        if (remainingAngle < 5)
        {
            snapToGrid();
            if (OTBhandler.checkOutOfBounds(this.transform.position))
            {
                isMoving = false;
            }
            else 
            {
                OTBhandler.isoutOfBounds = true;
                isMoving = false;
            }
            
        }
    }


    //method that flips the gravity for the cube depending on which wall is it on 
    private void flipCube() 
    {
        flipinggravity = true;
        UserData.IncrementInt(UserData.numOfGravitySwitched);
        //flip from ground to roof
        if (GameController.instance.IsInGround())
        {
            GameController.instance.InRoof();
        }
        //flip from roof to ground 
        else if (GameController.instance.IsInRoof())
        {
            GameController.instance.InGround();
        }
        //flip from right to left
        else if (GameController.instance.IsInRight())
        {
            GameController.instance.InLeft();
        }
        //flip from left to right 
        else if (GameController.instance.IsInLeft())
        {
            GameController.instance.InRight();
        }
        if (OTBhandler.checkOutOfBounds(this.transform.position))
        {
            
        }
        else
        {
            OTBhandler.isoutOfBounds = true;
            
        }
    }


    //a method that changed the constant force that is applied on the cube (gravity)
    private void changeGravity() 
    {
        //if statment that changes the force(gravity) depending on where the cube is touching 
        if (GameController.instance.IsInGround())
        {
            cubeRigidBody.AddForce(0, -9.81f, 0);
        }
        else if (GameController.instance.IsInRoof())
        {
            cubeRigidBody.AddForce(0, 9.81f, 0);
        }
        else if (GameController.instance.IsInRight())
        {
            cubeRigidBody.AddForce(9.81f, 0, 0);
        }
        else if (GameController.instance.IsInLeft())
        {
            cubeRigidBody.AddForce(-9.81f, 0, 0);
        }
    }

    //a method that sets the appropriate direction of the rotation/angle the cube will be moving on 
    private void setRotation(Vector3 direction) 
    {
        //checking if the jump ability is activited 
        if (AbilityController.instance.IsJumpActive() && !onCorner)
        {
            normalMovement = false;
            if (GameController.instance.IsInRoof())
            {
                rotationCenter = transform.localPosition + direction + Vector3.up; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.down, direction * 2 + Vector3.up); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.IsInRight())
            {
                rotationCenter = transform.localPosition + direction + Vector3.right; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.left, direction * 2 + Vector3.right); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.IsInLeft())
            {
                rotationCenter = transform.localPosition + direction + Vector3.left; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.right, direction * 2 + Vector3.left); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.IsInGround())
            {
                rotationCenter = transform.localPosition + direction + Vector3.down; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.up, direction * 2 + Vector3.down); // compute the rotation access based on the direction and y axis
            }
        }
        else
        {
            normalMovement = true;
            //if statment that changes the axes and the rotation depneing on which wall is the cube touching 
            if (GameController.instance.IsInRoof())
            {
                rotationCenter = transform.localPosition + direction / 2 + Vector3.up / 2; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.down, direction); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.IsInRight())
            {
                rotationCenter = transform.localPosition + direction / 2 + Vector3.right / 2; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.left, direction); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.IsInLeft())
            {
                rotationCenter = transform.localPosition + direction / 2 + Vector3.left / 2; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.right, direction); // compute the rotation access based on the direction and y axis
            }
            else if (GameController.instance.IsInGround())
            {
                rotationCenter = transform.localPosition + direction / 2 + Vector3.down / 2; // direction of the rotation
                rotationAxis = Vector3.Cross(Vector3.up, direction); // compute the rotation access based on the direction and y axis
            }
        }
        //print("axis"+rotationAxis);
        //print("center "+rotationCenter);
    }

    private void checkSpeedAbility() 
    {
        if (AbilityController.instance.IsSpeedActive())
        {
            speed = 500;
        }
        else if (AbilityController.instance.IsJumpActive())
        {
            speed = 350;
        }
        else 
        {
            speed = 300;
        }
    }


    //method to snap the cube to the grid
    private void snapToGrid()
    {
        UserData.IncrementInt(UserData.numOfCubeRolled);
        GameController.instance.rollsCount++;
        //checks if the cube is on the gound or roof
        if (GameController.instance.IsInGround()
           || GameController.instance.IsInRoof())
        {
            //creating the new location with round function and setting it in a vector
            var position = new Vector3(
                Mathf.RoundToInt(transform.position.x),
                transform.position.y,
                Mathf.RoundToInt(transform.position.z)
            );

            //settting the postion to a new rounded postion 
            this.transform.position = position;
        }

        //checks if the cube is on the left or right wall
        if (GameController.instance.IsInRight()
           || GameController.instance.IsInLeft())
        {

            //creating the new location with round function and setting it in a vector
            var position = new Vector3(
                transform.position.x,
                Mathf.RoundToInt(transform.position.y),
                Mathf.RoundToInt(transform.position.z)
            );

            //settting the postion to a new rounded postion
            this.transform.position = position;
        }
    }

    private void  checkMovement() 
    {
        if (AbilityController.instance.IsJumpActive() && remainingAngle>10)
        {
            if (onCorner)
            {
                checkmovement = false;
            }
            else
            {
                checkmovement = true;
            }
        }
        else
        {
            checkmovement = false;
        }
    }
}
