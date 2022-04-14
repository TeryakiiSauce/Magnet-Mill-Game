using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public static MainController instance;
    public RectTransform playRect;
    public GameObject tutorialBtn;
    public RectTransform instructionsRect;
    public RectTransform creditsRect;
    public RectTransform exitRect;
    private float[] yPositions = {270, 90, -90, -270};  //these variables will be used to place the buttons if the tutorial button is not active
    void Awake()
    {
        if (instance == null)   //checking if instance is null or not becuase we only need one instance of this class
        {
            instance = this;    
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        if(!UserData.GetBool(UserData.finishedTutorial))    //Checking if the user finished the tutorial, if not then deactivate the                                                       
        {                                                   //tutorial button, because the play button will redirect him to the tutorial
            tutorialBtn.SetActive(false);

            Vector3 tempPos = playRect.anchoredPosition;    //Repositioning every button by changing the y position to
            tempPos.y = yPositions[0];                      //maintain the gape of tutorial button, since its deactivated
            playRect.anchoredPosition = tempPos;

            tempPos = instructionsRect.anchoredPosition;
            tempPos.y = yPositions[1];
            instructionsRect.anchoredPosition = tempPos;

            tempPos = creditsRect.anchoredPosition;
            tempPos.y = yPositions[2];
            creditsRect.anchoredPosition = tempPos;

            tempPos = exitRect.anchoredPosition;
            tempPos.y = yPositions[3];
            exitRect.anchoredPosition = tempPos;
        }
    }

}
