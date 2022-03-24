using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesLoader : MonoBehaviour
{
    public static ScenesLoader instance;
    public Image blackImage;
    public float transitionSpeed;
    public enum WhichScene { MainMenu, Level0, Level1, Level2, Level3};
    private float copyTransSpeed;
    private string sceneName;
    private enum action { none, show, hide };
    private action currentAction;
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
        copyTransSpeed = transitionSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAction == action.show)
        {
            Color tempClr = blackImage.color;
            tempClr.a += Time.deltaTime * transitionSpeed;
            blackImage.color = tempClr;
            if(blackImage.color.a >= 1f)
            {
                SceneManager.LoadScene(sceneName);
                currentAction = action.hide;
            }
        }
        else if(currentAction == action.hide)
        {
            Color tempClr = blackImage.color;
            tempClr.a -= Time.deltaTime * transitionSpeed;
            blackImage.color = tempClr;
            if(blackImage.color.a <= 0f)
            {
                currentAction = action.none;
                blackImage.gameObject.SetActive(false);
                transitionSpeed = copyTransSpeed;
            }
        }
    }

    public void MoveToScene(WhichScene sceneName)
    {
        blackImage.gameObject.SetActive(true);
        currentAction = action.show;
        if(sceneName == WhichScene.MainMenu)
        {
            this.sceneName = "Main Menu";
        }
        else if(sceneName == WhichScene.Level0)
        {
            this.sceneName = "Level0";
        }
        else if(sceneName == WhichScene.Level1)
        {
            this.sceneName = "Level1";
        }
        else if (sceneName == WhichScene.Level2)
        {
            this.sceneName = "Level2";
        }
        else if (sceneName == WhichScene.Level3)
        {
            this.sceneName = "Level3";
        }
    }

    public void MoveToScene(WhichScene sceneName, float transitioningSpeed)
    {
        transitionSpeed = transitioningSpeed;
        MoveToScene(sceneName);
    }

    public bool IsTransitioning()
    {
        return currentAction != action.none;
    }
}
