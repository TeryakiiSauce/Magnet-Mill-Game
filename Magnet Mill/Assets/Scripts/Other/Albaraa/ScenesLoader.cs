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
    public enum WhichScene {MainMenu, Level0, Level1, Level2, Level3, Level4}; 
    private float copyTransSpeed;
    private string sceneName = "Main Menu";
    private enum Action { none, show, hide };
    private Action currentAction;
    void Awake()
    {
        if(instance == null)     //checking if instance is null or not becuase we only need one instance of this class
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);  //this will make the gameobject to move to all scenes not only main menu
        copyTransSpeed = transitionSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAction == Action.show)    //if the action on show, blackscreen background will start showing
        {
            Color tempClr = blackImage.color;
            tempClr.a += Time.deltaTime * transitionSpeed;
            blackImage.color = tempClr;
            if(blackImage.color.a >= 1f)    //check if blackscreen fully shown, then move to the next scene and start hiding it
            {
                SceneManager.LoadScene(sceneName);
                currentAction = Action.hide;
            }
        }
        else if(currentAction == Action.hide)   //if the action on hide, blackscreen background will start fading
        {
            Color tempClr = blackImage.color;
            tempClr.a -= Time.deltaTime * transitionSpeed;
            blackImage.color = tempClr;
            if(blackImage.color.a <= 0f) //checking if the blackscreen is fully hidden then change action to "none" which means finished
            {
                currentAction = Action.none;
                blackImage.gameObject.SetActive(false);
                transitionSpeed = copyTransSpeed;
            }
        }
    }

    public void MoveToScene(WhichScene sceneName)   //this function will be called from any script to move to any scene
    {                                               //usage: ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level0);
        blackImage.gameObject.SetActive(true);
        currentAction = Action.show;
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
        else if (sceneName == WhichScene.Level4)
        {
            this.sceneName = "Level4";
        }
    }

    public void MoveToScene(WhichScene sceneName, float transitioningSpeed) //same as the previous function but with this function you can specifiy the speed of transitioning
    {
        transitionSpeed = transitioningSpeed;
        MoveToScene(sceneName);
    }

    public void ReloadScene()
    {
        blackImage.gameObject.SetActive(true);
        currentAction = Action.show;
    }

    public void ReloadScene(float transitioningSpeed)
    {
        transitionSpeed = transitioningSpeed;
        ReloadScene();
    }

    public bool IsTransitioning()   //this function will be called to check wither the scene is transitioning or not,
    {                               //can be used to ignore buttons click events when transitioning
        return currentAction != Action.none;
    }
}
