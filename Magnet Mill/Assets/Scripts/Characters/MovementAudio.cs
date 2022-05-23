using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAudio : MonoBehaviour
{
    //private AudioSource audioSource;
    //public List<AudioClip> soundEffects;
    //private int[] CNumber = { 3, 4, 5 }; // C3, C4, C5
    //private int[] index = { 1, 2, 3, 4, 5, 6}; // Eg: C1.1, C1.2, C1.3, C1.4, C1.5, C1.6, etc.
    //public float volume = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.IsDead() || GameController.instance.IsLevelFinshed()
            || CubeController.isMoving || CubeController.flipinggravity)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space))
        {
            /*int randomSoundEffectIndex = Random.Range(0, soundEffects.Count);
            audioSource.PlayOneShot(soundEffects[randomSoundEffectIndex], volume);*/

            if (AudioManager.instance != null)
            {
                int tempCRange = Random.Range(3, 4); // Edit the range whenever needed
                int tempCounter = Random.Range(1, 6); // Edit the range whenever needed

                //AudioManager.instance.Play($"C{tempCRange}.{tempCounter}");
            }
        }

        /*if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            int randomSoundEffectIndex = Random.Range(0, soundEffects.Count);
            audioSource.PlayOneShot(soundEffects[randomSoundEffectIndex], volume);
        }*/
    }
}
