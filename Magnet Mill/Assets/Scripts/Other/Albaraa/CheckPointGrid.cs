using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointGrid : MonoBehaviour
{
    private bool isChecked;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isChecked || other.tag != "PlayerCube") return;
        GameController.instance.SetCheckPoint(gameObject.tag);
        isChecked = true;
    }
}
