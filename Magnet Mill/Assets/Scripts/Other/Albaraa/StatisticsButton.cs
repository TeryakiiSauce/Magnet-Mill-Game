using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsButton : MonoBehaviour
{
    public static bool isClicked;
    public GameObject statisticsBoard;
    private Button thisBtn;
    void Start()
    {
        isClicked = false;
        thisBtn = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isClicked && thisBtn.enabled)    //Disable button if it is clicked
        {
            thisBtn.enabled = false;
        }
        else if(!isClicked && !thisBtn.enabled)
        {
            thisBtn.enabled = true;
        }
    }

    public void ButtonClicked() //Will be called when the statistics button clicked
    {
        statisticsBoard.SetActive(true);    //Activate statistics board
        MainMenuBtn.BtnClicked = true;  //Preventing main menu buttons to be clicked
        isClicked = true;
    }
}
