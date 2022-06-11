using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAudio : MonoBehaviour
{
    public string soundName;
    public bool stopWhenFreeze;
    void Start()
    {
        AudioManager.instance.GetAudio(gameObject, soundName);  //get audio source from audio manager
        AudioManager.instance.Play(soundName);
    }

    void Update()
    {
        if (!stopWhenFreeze) return;

        if(AudioManager.instance.IsPlaying(soundName) && AbilityController.instance.IsFreezeActive())
        {
            AudioManager.instance.Stop(soundName);
        }
        else if(!AudioManager.instance.IsPlaying(soundName) && !AbilityController.instance.IsFreezeActive())
        {
            AudioManager.instance.Play(soundName);
        }
    }

}
