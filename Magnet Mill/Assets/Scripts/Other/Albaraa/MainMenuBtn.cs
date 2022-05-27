using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuBtn : MonoBehaviour
{
    public enum MenuBtn { play, tutorial, instruction, credits, exit };   //we will use this enum in the editor to choose which button is this
    public MenuBtn whichButton;
    public GameObject activateGameobject;
    public static bool BtnClicked;
    [HideInInspector][SerializeField] bool forTesting;
    [HideInInspector][SerializeField] string testSceneName;
    void Start()
    {
        BtnClicked = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClicked() //will be called when the button clicked
    {
        if (ScenesLoader.instance.IsTransitioning() || BtnClicked) return;  //checking first if any button is clicked

        if (whichButton == MenuBtn.play)    //Check which button is clicked
        {
            if (!forTesting)
            {
                if (UserData.GetBool(UserData.finishedTutorial))    //if user finished the tutorial activate the level selection menu
                {
                    activateGameobject.SetActive(true);
                }
                else                                              //if not load the tutorial scene
                {
                    ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level0);
                }
            }
            else if (testSceneName != null && testSceneName != "")
            {
                ScenesLoader.instance.TestingScene(testSceneName);      //load specific scene (for debuging)
            }
        }
        else if (whichButton == MenuBtn.tutorial)
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level0); //moving to scene "Level0"
        }
        else if (whichButton == MenuBtn.instruction || whichButton == MenuBtn.credits)
        {
            activateGameobject.SetActive(true);     //enable instructions or credits
        }
        else
        {
            Application.Quit();     //quit game
        }
        BtnClicked = true;  //this will switched to true to make sure that other main buttons unclickable
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(MainMenuBtn))]
    public class MainMenuBtnEditor : Editor  //Editor class to hide and display fields based on conditions in the inspector
    {
        public override void OnInspectorGUI()   //override OnInspectorGUI function from the base
        {
            MainMenuBtn btnSC = (MainMenuBtn)target;    //create instance on the targeted class

            base.OnInspectorGUI();      //call the base function to draw MainMenuBtn script variables in the inspector

            DrawDetails(btnSC);

            EditorUtility.SetDirty(btnSC);      //This will turn on saving for the added variables values on the
                                                //inspector when switching scenes
        }

        static void DrawDetails(MainMenuBtn btnSC)
        {
            if (btnSC.whichButton != MenuBtn.play)  //if the button is not the play button clear the boolean and return,
            {                                       //because we need to add variables dynamically only for the play button
                btnSC.forTesting = false;
                return;
            }

            EditorGUILayout.Space();            ///Adding space in the inspector

            EditorGUILayout.BeginHorizontal();      //start horizontal gui elements drawing
            //{
            EditorGUILayout.LabelField("Is for testing", GUILayout.MaxWidth(85));   //Add label with max width 85
            btnSC.forTesting = EditorGUILayout.Toggle(btnSC.forTesting);    //Added check box to assign fortesting variable
            //}
            EditorGUILayout.EndHorizontal();    //end horizontal gui elements drawing

            if (btnSC.forTesting)   //if checkbox is checked
            {
                EditorGUILayout.BeginHorizontal();
                //{
                EditorGUILayout.LabelField("Scene name (Exact!)");
                btnSC.testSceneName = EditorGUILayout.TextField(btnSC.testSceneName); //Add text field to assign testSceneName
                //}
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                btnSC.testSceneName = null;     //if forTesting checkbox is off clear testSceneName variable
            }

        }
    }

#endif

}
