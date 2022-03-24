using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSSettings : MonoBehaviour
{
    public int Target = 60;
    public static FPSSettings instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        QualitySettings.vSyncCount = 0;
    }

    void Update()
    {
        if (Application.targetFrameRate < Target)
        {
            Application.targetFrameRate = Target;
        }
    }
}
