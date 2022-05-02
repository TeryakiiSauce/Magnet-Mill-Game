using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAudio : MonoBehaviour
{
    //private AudioSource audioSource;
    //public List<AudioClip> soundEffects;
    private Sound[] soundEffects;
    //public float volume = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CubeController.isMoving || CubeController.flipinggravity)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space))
        {
            /*int randomSoundEffectIndex = Random.Range(0, soundEffects.Count);
            audioSource.PlayOneShot(soundEffects[randomSoundEffectIndex], volume);*/

            if (AudioManager.instance != null)
            {
                AudioManager.instance.Play("C3.1");
            }
        }

        /*if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            int randomSoundEffectIndex = Random.Range(0, soundEffects.Count);
            audioSource.PlayOneShot(soundEffects[randomSoundEffectIndex], volume);
        }*/
    }
}
