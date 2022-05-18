using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    //[SerializeField] private Text fPSLabel;
    [SerializeField] private float refreshRateInSeconds = 1f;

    private float timer;

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
