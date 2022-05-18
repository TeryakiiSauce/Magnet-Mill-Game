using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    //[SerializeField] private Text fPSLabel;
    /*[SerializeField]*/
    private float refreshRateInSeconds;

    private float timer;

    private void Start()
    {
        gameObject.SetActive(PlayerPrefs.GetInt("fpsToggle") != 0);
        refreshRateInSeconds = PlayerPrefs.GetFloat("fpsRefreshRate");
    }

    private void Update()
    {
        if (Time.unscaledTime > timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            //fPSLabel.text = "FPS: " + fps;
            GetComponent<TextMeshProUGUI>().text = fps.ToString();
            timer = Time.unscaledTime + refreshRateInSeconds;
        }
    }
}
