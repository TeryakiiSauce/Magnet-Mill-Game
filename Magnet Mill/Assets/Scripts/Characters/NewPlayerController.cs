using System.Collections;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    public static bool hasHitWall = false;

    public float rollSpeed = 3;
    public float pivotPointOffset = 1f;

    private bool isMoving;

    private void Update()
    {
        // So that it doesn't interrupt the rotation process
        if (isMoving || hasHitWall) return;

        // On ground
        if (Input.GetKey(KeyCode.W))
        {
            GridSystem.currentCubePosition.z += 1;
            Rotate(Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            GridSystem.currentCubePosition.x -= 1;
            Rotate(Vector3.left);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            GridSystem.currentCubePosition.z -= 1;
            Rotate(Vector3.back);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            GridSystem.currentCubePosition.x += 1;
            Rotate(Vector3.right);
        }
    }

    private void Rotate(Vector3 direction)
    {
        Vector3 pivotPoint = transform.position + (Vector3.down + direction) * pivotPointOffset;
        Vector3 axis = Vector3.Cross(Vector3.up, direction);

        GridSystem.CheckHitWall(); // Automatically does everything and stops the cube from moving if it hits a wall.
        StartCoroutine(Roll(pivotPoint, axis));

        Debug.Log("Current position: " + GridSystem.currentCubePosition);
    }

    private IEnumerator Roll(Vector3 pivotPoint, Vector3 axis)
    {
        isMoving = true;

        // Repeats until rotation reaches 90 degrees
        for (var i = 0; i < 90 / rollSpeed; i++)
        {
            transform.RotateAround(pivotPoint, axis, rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        isMoving = false;
    }
}