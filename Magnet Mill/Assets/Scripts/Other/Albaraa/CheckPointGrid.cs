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
        if (isChecked || other.tag != "PlayerCube") return;
        thisMRenderer.material = GameController.instance.checkPointOnMaterial;
        GameController.instance.SetCheckPoint(gameObject.tag);
        isChecked = true;
    }
}
