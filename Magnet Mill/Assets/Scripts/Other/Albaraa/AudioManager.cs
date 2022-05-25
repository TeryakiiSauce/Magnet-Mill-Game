using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup[] audioMixerGroups;
    private float[] volumes;
    public static AudioManager instance;
    private bool Muted = false;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)    //checking if instance is null or not becuase we only need one instance of this class
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);  //this will make the gameobject to move to all scenes not only main menu

        volumes = new float[sounds.Length];

        int IND = 0;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            volumes[IND] = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            SetAudioOutputMixerGroup(IND); // Function uses hard-coded method of adding audio mixer groups to sounds because I couldn't find another way to make them displayed in the inspector menu
            IND++;
        }

        if (PlayerPrefs.GetFloat("audioVolume") == 0f)
        {
            Muted = true;
            MuteAll();
        }

        //SetAudioVolumeLevel();

        // Removed this method because the audio settings should be received from the settings menu values
        /*if (UserData.GetBool(UserData.isMuted))
        {
            MuteAll();
        }*/
    }

    void Start()
    {

    }

    public void Play(string name)  //this function will play any sound by its name, usage: AudioManager.instance.Play("soundName");
    {
        if (Muted)
        {
            return;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Play();

    }
    public void Stop(string name)   //this function will stop any sound by its name, usage: AudioManager.instance.Stop("soundName"); 
    {
        if (Muted)
        {
            return;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    public void AddVolume(string name, float amount)    //this function will add volume to any sound by its name
    {                                                       //usage: AudioManager.instance.AddVolume("soundName", 0.5f);
        if (Muted)
        {
            return;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        if (s.volume + amount >= 1f)
        {
            s.volume = 1f;
            s.source.volume = 1f;
        }
        else if (s.volume + amount <= 0f)
        {
            s.volume = 0f;
            s.source.volume = 0f;
        }
        else
        {
            s.volume += amount;
            s.source.volume += amount;
        }
    }

    // public void SetVolume(string name, float volumeLevel)
    // {
    //     if (Muted)
    //     {
    //         return;
    //     }
    //     Sound s = Array.Find(sounds, sound => sound.name == name);
    //     if (s == null)
    //     {
    //         Debug.LogWarning("Sound " + name + " not found!");
    //         return;
    //     }
    //     if (volumeLevel < 0)
    //     {
    //         volumeLevel = 0;
    //     }
    //     else if (volumeLevel > 1)
    //     {
    //         volumeLevel = 1;
    //     }
    //     s.volume = volumeLevel;
    //     s.source.volume = volumeLevel;
    // }
    public bool IsVolumeMax(string name)    //used to check the volume of a sound if it is max
    {
        if (Muted)
        {
            return false;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return false;
        }
        if (s.volume >= 1f)
        {
            return true;
        }
        return false;
    }
    public bool IsPlaying(string name)  //used to check whether the sound is playing or not
    {
        if (Muted)
        {
            return false;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return false;
        }
        return s.source.isPlaying;
    }
    public float GetVolume(string name) //get volume of certain sound (between 0 and 1)
    {
        if (Muted)
        {
            return 0;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return 0;
        }
        return s.source.volume;
    }
    public void MuteAll()   //used to mute the game
    {
        Muted = true;
        UserData.SetBool(UserData.isMuted, true);
        foreach (Sound s in sounds)
        {
            s.volume = 0f;
            s.source.volume = 0f;
        }
    }

    public void UnMuteAll() //used to unmute the game
    {
        Muted = false;
        UserData.SetBool(UserData.isMuted, false);
        int IND = 0;
        foreach (Sound s in sounds)
        {
            s.volume = volumes[IND];
            s.source.volume = volumes[IND];
            IND++;
        }

    }

    public void SetAudioVolumeLevel(string args)
    {
        if (PlayerPrefs.GetFloat("audioVolume") == 0f)
        {
            MuteAll();
            return;
        }
        else if (PlayerPrefs.GetFloat("audioVolume") == 100f)
        {
            UnMuteAll();
            return;
        }

        UnMuteAll(); // So that the mute icon refreshes

        float[] tempVolumes = new float[sounds.Length];
        int IND = 0;

        switch (args)
        {
            case "verbose-only":
                foreach (Sound s in sounds)
                {
                    tempVolumes[IND] = GetVolume(s.name);

                    Debug.Log($"The sound's ({s.name}) volume has been changed from {tempVolumes[IND]} ({tempVolumes[IND] * 100}%) --> TO --> {(s.source.volume * PlayerPrefs.GetFloat("audioVolume")) / 100} ({((s.source.volume * PlayerPrefs.GetFloat("audioVolume")) / 100) * 100}%)");

                    IND++;
                }
                break;

            default:
                foreach (Sound s in sounds)
                {
                    tempVolumes[IND] = GetVolume(s.name);
                    s.volume = (s.volume * PlayerPrefs.GetFloat("audioVolume")) / 100;
                    s.source.volume = (s.source.volume * PlayerPrefs.GetFloat("audioVolume")) / 100;

                    //Debug.Log($"The sound's ({s.name}) volume has been changed from {tempVolumes[IND]} ({tempVolumes[IND] * 100}%) --> TO --> {s.volume} ({s.volume * 100}%)"); // don't use this one; saved just in case.

                    // Use the following debug code instead
                    //Debug.Log($"The sound's ({s.name}) volume has been changed from {tempVolumes[IND]} ({tempVolumes[IND] * 100}%) --> TO --> {s.source.volume} ({s.source.volume * 100}%)");

                    IND++;
                }
                break;
        }
    }

    private void SetAudioOutputMixerGroup(int index)
    {
        /*
        Audio Mixer Groups keys codes:
        0 = Background
        1 = Cube Movement
        2 = Cube Flipped
        3 = Win / Fail
        4 = Checkpoint
        5 = Misc
        */

        switch (index)
        {
            case 0:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[0];
                break;
            case 1:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[0];
                break;
            case 2:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[0];
                break;
            case 3:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[0];
                break;
            case 4:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[0];
                break;
            case 5:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[0];
                break;
            case 6:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[1];
                break;
            case 7:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[1];
                break;
            case 8:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[2];
                break;
            case 9:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[3];
                break;
            case 10:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[3];
                break;
            case 11:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[5];
                break;
            case 12:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[5];
                break;
            case 13:
                sounds[index].source.outputAudioMixerGroup = audioMixerGroups[4];
                break;
        }
    }
    public bool IsMuted()
    {
        return Muted;
    }
}
