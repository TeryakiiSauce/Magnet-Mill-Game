using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAudio : MonoBehaviour
{
    public string soundName;
    void Start()
    {
        AudioManager.instance.GetAudio(gameObject, soundName);
    }
}
