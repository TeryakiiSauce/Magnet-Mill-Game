using UnityEditor;
using UnityEngine;

public class GetObjectFromGUID : MonoBehaviour
{
    public string gameObjectGUID;

    private void Start()
    {
        Debug.Log(AssetDatabase.GUIDToAssetPath(gameObjectGUID));
    }
}
