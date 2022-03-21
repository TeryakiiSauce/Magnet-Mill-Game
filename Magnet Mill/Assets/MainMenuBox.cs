using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBox : MonoBehaviour
{
    public float speed;
    public enum Direction { toRight, toLeft};
    public Direction boxDirection;
    public bool isUp;
    private float rightMaxX = 24;
    private float rightStartX = -17;
    private float leftMinX = -20;
    private float leftStartX = 24;
    private float timer;
    private Rigidbody body;
    private ConstantForce constF;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        constF = GetComponent<ConstantForce>();
        Collider thisCld = GetComponent<Collider>();
        GameObject[] allBoxes = GameObject.FindGameObjectsWithTag("MainMenuBox");
        foreach (GameObject box in allBoxes)
        {
            Physics.IgnoreCollision(box.GetComponent<Collider>(), thisCld);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isUp)
        {
            if(constF.force.y != 50) constF.force = new Vector3(0, 50, 0);
        }
        else
        {
            if(constF.force.y != -50) constF.force = new Vector3(0, -50, 0);
        }

        if(timer < 1.8f)
        {
            timer += Time.deltaTime;
            return;
        }
        isUp = !isUp;
        timer = 0;
    }

    void FixedUpdate()
    {
        Vector3 newPos = transform.localPosition;
        if (boxDirection == Direction.toRight)
        {
            newPos.x += speed * Time.fixedDeltaTime;
            transform.localPosition = newPos;
            if (transform.localPosition.x > rightMaxX)
            {
                transform.localPosition = new Vector3(rightStartX, transform.localPosition.y, 0);
            }
        }
        else
        {
            newPos.x -= speed * Time.fixedDeltaTime;
            transform.localPosition = newPos;
            if (transform.localPosition.x < leftMinX)
            {
                transform.localPosition = new Vector3(leftStartX, transform.localPosition.y, 0);
            }
        }
    }

}
