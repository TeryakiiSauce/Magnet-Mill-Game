using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRoll : MonoBehaviour
{
    public float rollSpeed = 5f;

    private GameObject[] cubeFaces;

    // Start is called before the first frame update
    void Start()
    {
        cubeFaces = GameObject.FindGameObjectsWithTag("CubeFace");

        foreach (GameObject face in cubeFaces)
        {
            string tempString = $"{face.name} has the following children: (click to view more)\n";

            int totalChildren = face.transform.childCount;
            for (int i = 0; i < totalChildren; i++)
            {
                tempString += $"{face.transform.GetChild(i).name}\n";
            }

            Debug.Log(tempString);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
