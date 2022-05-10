using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHolder : MonoBehaviour
{
    public GameObject[] items;
    public bool isInTutorial;
    void Awake()
    {
        if (isInTutorial) return;
        int randIND = Random.Range(0, items.Length);
        items[randIND].SetActive(true);
    }

}
