using System.Collections;
using System.Collections.Generic;
using UnityEditor;

#if UNITY_EDITOR
    using UnityEngine;
#endif

public class MainMenuBtn : MonoBehaviour
{
    public enum MenuBtn {play, tutorial, instruction, credits, exit};   //we will use this enum in the editor to choose which button is this
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

        if(whichButton == MenuBtn.play)
        {
            if (!forTesting)
            {
                ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level0); //moving to scene "Level0"
            }
            else if (testSceneName != null && testSceneName != "")
            {
                ScenesLoader.instance.TestingScene(testSceneName);
            }
            //ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level0BaraaTemp);
        }
        else if(whichButton == MenuBtn.tutorial)
        {
            ScenesLoader.instance.MoveToScene(ScenesLoader.WhichScene.Level0); //moving to scene "Level0"
        }
        else if(whichButton == MenuBtn.instruction)
        {
            activateGameobject.SetActive(true);     //enable instructions
        }
        else if(whichButton == MenuBtn.credits)
        {
            activateGameobject.SetActive(true);     //enable credits         
        }
        else
        {
            Application.Quit();     //quit game
        }
        BtnClicked = true;  //this will switched to true to make sure that other main buttons unclickable
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(MainMenuBtn))]
    public class MainMenuBtnEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            MainMenuBtn btnSC = (MainMenuBtn)target;
            

            base.OnInspectorGUI();

            DrawDetails(btnSC);

            //EditorUtility.SetDirty(target);
            
        }

        static void DrawDetails(MainMenuBtn btnSC)
        {
            if (btnSC.whichButton != MenuBtn.play) return;
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            //{
            EditorGUILayout.LabelField("Is for testing", GUILayout.MaxWidth(85));
            btnSC.forTesting = EditorGUILayout.Toggle(btnSC.forTesting);
            //}
            EditorGUILayout.EndHorizontal();

            if(btnSC.forTesting)
            {
                EditorGUILayout.BeginHorizontal();
                //{
                EditorGUILayout.LabelField("Scene name (Exact!)");
                btnSC.testSceneName = EditorGUILayout.TextField(btnSC.testSceneName);
                //}
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                btnSC.testSceneName = null;
            }
            
        }
    }

#endif

}
