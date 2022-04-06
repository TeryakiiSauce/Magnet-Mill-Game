using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideInstructions()  //this function will be called when the player clicked the instructions to hide them
    {
        MainMenuBtn.BtnClicked = false;
        gameObject.SetActive(false);
    }
}
