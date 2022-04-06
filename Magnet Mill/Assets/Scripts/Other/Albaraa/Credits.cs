using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreditsClicked()    //this function will be called when the credits clicked
    {
        MainMenuBtn.BtnClicked = false;
        transform.parent.gameObject.SetActive(false);
    }
}
