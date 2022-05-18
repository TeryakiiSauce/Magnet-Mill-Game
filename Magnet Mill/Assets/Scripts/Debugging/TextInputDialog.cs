using System;
using UnityEditor;
using UnityEngine;

namespace com.hololabs
{
    public class TextInputDialog : EditorWindow
    {
        const string INPUT_NAME = "TextInputDialog_TextField";
        public string promptText = "Enter your input:";
        public Action<string> callback;

        void OnGUI()
        {
            EditorGUILayout.BeginVertical(GUIStyle.none, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            EditorGUILayout.LabelField(promptText);
            GUI.SetNextControlName(INPUT_NAME);
            string inputText = EditorGUILayout.DelayedTextField("");
            EditorGUILayout.EndVertical();

            if (string.IsNullOrEmpty(inputText))
            {
                EditorGUI.FocusTextInControl(INPUT_NAME);
            }
            else
            {
                callback(inputText);
                Close();
            }
        }

        void OnLostFocus()
        {
            Close();
        }

        public static void Prompt(string title, string promptText, Action<string> callback)
        {
            var window = CreateInstance<TextInputDialog>();
            window.minSize = new Vector2(300, 50);
            window.maxSize = window.minSize;
            window.titleContent = new GUIContent(title);
            window.callback = callback;
            window.promptText = promptText;
            window.ShowUtility();
        }
    }
}