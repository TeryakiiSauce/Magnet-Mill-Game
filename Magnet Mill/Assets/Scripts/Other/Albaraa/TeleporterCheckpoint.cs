using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterCheckpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PlayerCube") return;
        print("Player is out of map !!!");
        GameController.instance.OutOffMap();
    }
}
