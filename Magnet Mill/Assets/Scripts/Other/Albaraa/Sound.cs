 using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound {    //this class will be used to control sounds in the AudioManager class (each sound will have instance of this class)

    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
    public bool loop;
}
