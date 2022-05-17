using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParallax : MonoBehaviour
{
    private Vector3 StartPos, contentPosition;

    //public float strength = 20;
    private float strength;

    // Start is called before the first frame update
    void Start()
    {
        strength = PlayerPrefs.GetInt("mouseParallax") / 5f;
        //print("strength: " + strength * 5);
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        contentPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        contentPosition.z = 0; // We don't need to move in the Z-axis

        transform.position = new Vector3(StartPos.x + (contentPosition.x * strength), StartPos.y + (contentPosition.y * strength), 0);
    }
}
