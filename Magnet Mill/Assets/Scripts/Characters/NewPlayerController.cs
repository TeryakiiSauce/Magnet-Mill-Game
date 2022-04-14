using System.Collections;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    public float rollSpeed = 3;
    public float pivotPointOffset = 1f;

    public static bool isMoving;

    private void Update()
    {
        // So that it doesn't interrupt the rotation process
        if (isMoving) return;

        // On ground
        if (Input.GetKey(KeyCode.W)) Rotate(Vector3.forward);
        else if (Input.GetKey(KeyCode.A)) Rotate(Vector3.left);
        else if (Input.GetKey(KeyCode.S)) Rotate(Vector3.back);
        else if (Input.GetKey(KeyCode.D)) Rotate(Vector3.right);
    }

    private void Rotate(Vector3 direction)
    {
        Vector3 pivotPoint = transform.position + (Vector3.down + direction) * pivotPointOffset;
        Vector3 axis = Vector3.Cross(Vector3.up, direction);

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