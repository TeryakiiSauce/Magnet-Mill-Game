using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointGrid : MonoBehaviour
{
    private MeshRenderer thisMRenderer;
    private bool isChecked;
    void Start()
    {
        thisMRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isChecked || other.tag != "PlayerCube") return;     //Check if collided object is not playercube or the check point is already active, if yes return
        thisMRenderer.material = GameController.instance.checkPointOnMaterial;  //Taking "ON" material from game controller object
        GameController.instance.SetCheckPoint(gameObject.tag);  //Set checkpoint as a current checkpoint
        isChecked = true;
    }
}
