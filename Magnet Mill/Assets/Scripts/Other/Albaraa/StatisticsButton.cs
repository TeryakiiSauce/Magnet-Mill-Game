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
        if(isClicked && thisBtn.enabled)
        {
            thisBtn.enabled = false;
        }
        else if(!isClicked && !thisBtn.enabled)
        {
            thisBtn.enabled = true;
        }
    }

    public void ButtonClicked()
    {
        statisticsBoard.SetActive(true);
        MainMenuBtn.BtnClicked = true;
        isClicked = true;
    }
}
