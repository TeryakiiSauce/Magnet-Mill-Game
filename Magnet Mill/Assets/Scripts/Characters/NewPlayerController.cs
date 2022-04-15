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
        GridSystem.CheckHitWall(); // Automatically does everything and stops the cube from moving if it hits a wall.

        // So that it doesn't interrupt the rotation process
        if (isMoving || hasHitWall) return;

        if (GridSystem.isHorizontal)
        {
            if (Input.GetKey(KeyCode.W))
            {
                GridSystem.currentCubePosition.z += 1;
                Debug.Log("Current position: " + GridSystem.currentCubePosition);

                // There was a bug that made the cube go beyond the walls
                if (GridSystem.currentCubePosition.z <= GridSystem.gridsZ.Length - 1)
                {
                    Rotate(Vector3.forward);
                }
                else
                {
                    GridSystem.currentCubePosition.z = GridSystem.gridsZ.Length - 1;
                }
            }

            else if (Input.GetKey(KeyCode.A))
            {
                GridSystem.currentCubePosition.x -= 1;
                Debug.Log("Current position: " + GridSystem.currentCubePosition);

                // There was a bug that made the cube go beyond the walls
                if (GridSystem.currentCubePosition.x >= 0)
                {
                    Rotate(Vector3.left);
                }
                else
                {
                    GridSystem.currentCubePosition.x = 0;
                }
            }

            else if (Input.GetKey(KeyCode.S))
            {
                GridSystem.currentCubePosition.z -= 1;
                Debug.Log("Current position: " + GridSystem.currentCubePosition);

                // There was a bug that made the cube go beyond the walls
                if (GridSystem.currentCubePosition.z >= 0)
                {
                    Rotate(Vector3.back);
                }
                else
                {
                    GridSystem.currentCubePosition.z = 0;
                }
            }

            else if (Input.GetKey(KeyCode.D))
            {
                GridSystem.currentCubePosition.x += 1;
                Debug.Log("Current position: " + GridSystem.currentCubePosition);

                // There was a bug that made the cube go beyond the walls
                if (GridSystem.currentCubePosition.x <= GridSystem.gridsX.Length - 1)
                {
                    Rotate(Vector3.right);
                }
                else
                {
                    GridSystem.currentCubePosition.x = GridSystem.gridsX.Length - 1;
                }
            }
        }

        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                GridSystem.currentCubePosition.z += 1;
                Debug.Log("Current position: " + GridSystem.currentCubePosition);

                // There was a bug that made the cube go beyond the walls
                if (GridSystem.currentCubePosition.z <= GridSystem.gridsZ.Length - 1)
                {
                    Rotate(Vector3.forward);
                }
                else
                {
                    GridSystem.currentCubePosition.z = GridSystem.gridsZ.Length - 1;
                }
            }

            else if (Input.GetKey(KeyCode.A))
            {
                GridSystem.currentCubePosition.x -= 1;
                Debug.Log("Current position: " + GridSystem.currentCubePosition);

                // There was a bug that made the cube go beyond the walls
                if (GridSystem.currentCubePosition.x >= 0)
                {
                    Rotate(Vector3.left);
                }
                else
                {
                    GridSystem.currentCubePosition.x = 0;
                }
            }

            else if (Input.GetKey(KeyCode.S))
            {
                GridSystem.currentCubePosition.z -= 1;
                Debug.Log("Current position: " + GridSystem.currentCubePosition);

                // There was a bug that made the cube go beyond the walls
                if (GridSystem.currentCubePosition.z >= 0)
                {
                    Rotate(Vector3.back);
                }
                else
                {
                    GridSystem.currentCubePosition.z = 0;
                }
            }

            else if (Input.GetKey(KeyCode.D))
            {
                GridSystem.currentCubePosition.x += 1;
                Debug.Log("Current position: " + GridSystem.currentCubePosition);

                // There was a bug that made the cube go beyond the walls
                if (GridSystem.currentCubePosition.x <= GridSystem.gridsX.Length - 1)
                {
                    Rotate(Vector3.right);
                }
                else
                {
                    GridSystem.currentCubePosition.x = GridSystem.gridsX.Length - 1;
                }
            }
        }
    }

    private void Rotate(Vector3 direction)
    {
        Vector3 pivotPoint = new Vector3();
        Vector3 axis = new Vector3();

        if (GridSystem.isHorizontal)
        {
            pivotPoint = transform.position + (Vector3.down + direction) * pivotPointOffset;
            axis = Vector3.Cross(Vector3.up, direction);
        }
        else
        {
            pivotPoint = transform.position + (Vector3.left + direction) * pivotPointOffset;
            axis = Vector3.Cross(Vector3.right, direction);
        }

        StartCoroutine(Roll(pivotPoint, axis));
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