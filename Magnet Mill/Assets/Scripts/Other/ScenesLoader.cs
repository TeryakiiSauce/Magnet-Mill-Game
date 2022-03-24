using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesLoader : MonoBehaviour
{
    public static ScenesLoader instance;
    public Image blackImage;
    public bool isTransitioning;
    private string sceneName;
    void Awake()
    {
        if(instance == null)
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isTransitioning && blackImage.color.a < 1)
        {
            Color tempClr = blackImage.color;
            tempClr.a += Time.deltaTime;
            blackImage.color = tempClr;
            if(blackImage.color.a >= 1)
            {
                SceneManager.LoadScene(sceneName);
                isTransitioning = false;            
            }
        }
    }

    public void MoveToScene(string sceneName)
    {
        isTransitioning = true;
        this.sceneName = sceneName;
    }
}
