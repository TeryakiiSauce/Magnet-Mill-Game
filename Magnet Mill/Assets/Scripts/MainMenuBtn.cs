using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBtn : MonoBehaviour
{
    
    public Sprite selectedSP;
    public enum MenuBtn { play};
    public MenuBtn whichButton;

    private Sprite nrmlSP;
    private SpriteRenderer thisSPR;

    void Start()
    {
        thisSPR = GetComponent<SpriteRenderer>();
        nrmlSP = thisSPR.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBlue()
    {
        if (thisSPR.sprite != selectedSP) thisSPR.sprite = selectedSP;
        //print("setBlue Called :)");
    }

    public void setWhite()
    {
        if (thisSPR.sprite != nrmlSP) thisSPR.sprite = nrmlSP;
        //print("setWhite Called :)");
    }
}
