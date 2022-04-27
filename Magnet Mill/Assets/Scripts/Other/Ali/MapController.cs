using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapController : MonoBehaviour
{
    public GameObject mapBox;
    public GameObject mapKeys;
    public Vector3 tempQ;
    public float endZAngle = 10;
    public int rotationSpeed = 100;

    public RectTransform rect;
     
    private void Update()
    {
        Rotate();
    }


    private void Rotate()
    {
        tempQ = rect.eulerAngles;

        if (tempQ.z < endZAngle && !Mathf.Approximately(tempQ.z,endZAngle))
        {
            tempQ.z += rotationSpeed * Time.deltaTime;
            rect.eulerAngles = tempQ;

        }
        else if(tempQ.z > endZAngle) 
        {
            tempQ.z = endZAngle;
            rect.eulerAngles = tempQ;

        }


    }
}
