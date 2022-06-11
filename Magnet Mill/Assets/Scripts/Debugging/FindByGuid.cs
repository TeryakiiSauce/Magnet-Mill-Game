#if (UNITY_EDITOR) 

using UnityEditor;
using UnityEngine;

namespace com.hololabs.editor
{
    public class FindByGuid : EditorWindow
    {
        [MenuItem("Utility/Find Asset by Guid %&g")]
        public static void DoFindByGuidMenu()
        {
            TextInputDialog.Prompt("GUID", "Find asset by Guid:", FindAssetByGuid);
        }

        static void FindAssetByGuid(string searchGuid)
        {
            string path = AssetDatabase.GUIDToAssetPath(searchGuid);
            if (string.IsNullOrEmpty(path)) return;
            var obj = AssetDatabase.LoadAssetAtPath<Object>(path);
            if (obj == null) return;

            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(obj);
        }
    }
}

#endif