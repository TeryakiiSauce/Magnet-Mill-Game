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
    public bool playFromObj;    //if this true it means the sound will not be played fronm the audio manager, it will be played from
}                               //a specific game object
