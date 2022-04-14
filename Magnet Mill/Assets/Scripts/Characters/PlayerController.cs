using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int gridSteps = 2;
    public float movementSpeed = 0.2f;

    private bool isMoving = false;
    private Vector3 originalPosition, finalPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Return if player is moving
        if (isMoving) return;

        // Continue if player is not moving
        if (Input.GetKey(KeyCode.W)) StartCoroutine(MovePlayer(Vector3.forward));

        if (Input.GetKey(KeyCode.A)) StartCoroutine(MovePlayer(Vector3.left));

        if (Input.GetKey(KeyCode.S)) StartCoroutine(MovePlayer(Vector3.back));

        if (Input.GetKey(KeyCode.D)) StartCoroutine(MovePlayer(Vector3.right));
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0f; // To count the time elapsed since the method is called

        originalPosition = transform.position;
        finalPosition = originalPosition + (direction * gridSteps);

        while (elapsedTime < movementSpeed)
        {
            transform.position = Vector3.Lerp(originalPosition, finalPosition, (elapsedTime / movementSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = finalPosition;

        isMoving = false;
    }
}
