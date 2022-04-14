using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    public int speed = 300;
    bool isMoving = false;
    private Rigidbody cubeRigidBody;
    private bool onGround = true;
    private bool onRoof = false;
    private bool onRightWall = false;
    private bool onLeftWall = false;

    // Start is called before the first frame update
    void Start()
    {
        // Choose one depending on needs
        //cubeRigidBody = GetComponent<Rigidbody>();
        cubeRigidBody = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMoving) return; // to prevent rolling when we are in the middle of a roll

        
        if (onGround)
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.left)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.right)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }
        else if (onRoof)
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.left)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.right)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }
        else if (onRightWall)
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.down)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.up)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }
        else if (onLeftWall)
        {
            if (Input.GetKey(KeyCode.A)) StartCoroutine(Roll(Vector3.up)); // rotate to the left when A clicked
            else if (Input.GetKey(KeyCode.D)) StartCoroutine(Roll(Vector3.down)); // rotate to the right when D clicked
            else if (Input.GetKey(KeyCode.W)) StartCoroutine(Roll(Vector3.forward)); // rotate forward when W clicked
            else if (Input.GetKey(KeyCode.S)) StartCoroutine(Roll(Vector3.back)); // rotate back when S clicked
        }
        if (Input.GetKey(KeyCode.Space)) 
        {
            if (onGround)
            {
                onGround = false;
                onRoof = true;
            }
            else if (onRoof)
            {
                onGround = true;
                onRoof = false;

            }
            else if (onRightWall)
            {
                onRightWall = false;
                onLeftWall = true;
            }
            else if (onLeftWall)
            {
                onLeftWall = false;
                onRightWall = true;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (other.tag == "Right wall")
        {
            onGround = false;
            onRoof = false;
            onRightWall = true;
            onLeftWall = false;
        }
        else if(other.tag == "Left wall")
        {
            onGround = false;
            onRoof = false;
            onRightWall = false;
            onLeftWall = true;
        }
        else if (other.tag == "Roof")
        {
            onGround = false;
            onRoof = true;
            onRightWall = false;
            onLeftWall = false;
        }
        else if (other.tag == "Ground")
        {
            onGround = true;
            onRoof = false;
            onRightWall = false;
            onLeftWall = false;
        }
    }
    void FixedUpdate()
    {

        if (onGround)
        {
            
            cubeRigidBody.AddForce(0, -9.81f, 0);
        }
        else if (onRoof) 
        {
           ;
            cubeRigidBody.AddForce(0, 9.81f, 0);
            
        }
        else if (onRightWall)
        {
            
            cubeRigidBody.AddForce(9.81f, 0, 0);
        }
        else if (onLeftWall)
        {
            
            cubeRigidBody.AddForce(-9.81f, 0, 0);
        }
    }

    IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;

        float remainingAngle = 90;

        
            Vector3 rotationCenter = transform.localPosition + direction / 2 + Vector3.down / 2; // direction of the rotation
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction); // compute the rotation access based on the direction and y axis
         if (onRoof)
        {
             rotationCenter = transform.localPosition + direction / 2 + Vector3.up / 2; // direction of the rotation
             rotationAxis = Vector3.Cross(Vector3.down, direction); // compute the rotation access based on the direction and y axis
        }
        else if (onRightWall)
        {
             rotationCenter = transform.localPosition + direction / 2 + Vector3.right / 2; // direction of the rotation
             rotationAxis = Vector3.Cross(Vector3.left, direction); // compute the rotation access based on the direction and y axis
        }
        else if (onLeftWall)
        {
             rotationCenter = transform.localPosition + direction / 2 + Vector3.left / 2; // direction of the rotation
             rotationAxis = Vector3.Cross(Vector3.right, direction); // compute the rotation access based on the direction and y axis
        }
        else if(onGround)
        {
            rotationCenter = transform.localPosition + direction / 2 + Vector3.down / 2; // direction of the rotation
            rotationAxis = Vector3.Cross(Vector3.up, direction); // compute the rotation access based on the direction and y axis
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

        isMoving = false;
    }
}
