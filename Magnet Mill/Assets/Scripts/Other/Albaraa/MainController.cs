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
    private float[] yPositions = {270, 90, -90, -270};
    void Awake()
    {
        if (instance == null)
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
        if(!UserData.GetBool(UserData.finishedTutorial))
        {
            tutorialBtn.SetActive(false);

            Vector3 tempPos = playRect.anchoredPosition;
            tempPos.y = yPositions[0];
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
