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
    private float timer = 1.7f;
    private Rigidbody body;
    private ConstantForce constF;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        constF = GetComponent<ConstantForce>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUp)
        {
            constF.force = new Vector3(0, 50, 0);
        }
        else
        {
            constF.force = new Vector3(0, -50, 0);
        }
        Vector3 newPos = transform.localPosition;
        if(boxDirection == Direction.toRight)
        {
            newPos.x+= speed * Time.deltaTime;
            transform.localPosition = newPos;
            if (transform.localPosition.x > rightMaxX)
            {
                transform.localPosition = new Vector3(rightStartX, 0.8f, transform.localPosition.x);
            }
        }
        else
        {
            newPos.x -= speed * Time.deltaTime;
            transform.localPosition = newPos;
            if (transform.localPosition.x < leftMinX)
            {
                transform.localPosition = new Vector3(leftStartX, 0.8f, transform.localPosition.x);
            }
        }

        if(timer < 1.7f)
        {
            timer += Time.deltaTime;
            return;
        }
        isUp = !isUp;
        timer = 0;
    }
}
