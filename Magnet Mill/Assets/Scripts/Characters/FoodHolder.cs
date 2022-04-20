using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHolder : MonoBehaviour
{
    public GameObject[] items;
    void Awake()
    {
        int randIND = Random.Range(0, items.Length);
        items[randIND].SetActive(true);
    }

}
