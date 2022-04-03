using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    private float[] volumes;
    public static AudioManager instance;
    private bool Muted = false;

    // Use this for initialization
	void Awake () {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        volumes = new float[sounds.Length];
        int IND = 0;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            volumes[IND] = s.volume;
            IND++;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        if (PlayerPrefs.GetInt("Muted") == 1)
        {
            MuteAll();
        }
    }

    void Start ()
    {
        ///Play("THEME");
    }

    public void Play (string name)
    {
        if(Muted)
        {
            return;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Play();

    }
    public void Stop(string name)
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
    public void AddVolume(string name, float amount)
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
        if (s.volume + amount >= 1f)
        {
            s.volume = 1f;
            s.source.volume = 1f;
        }
        else if(s.volume + amount <= 0f)
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
    public bool IsVolumeMax(string name)
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
        if(s.volume >= 1f)
        {
            return true;
        }
        return false;
    }
    public bool IsPlaying(string name)
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
    public float GetVolume(string name)
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
    public void MuteAll()
    {
        Muted = true;
        foreach(Sound s in sounds)
        {
            s.volume = 0f;
            s.source.volume = 0f;
        }
    }
    public void UnMuteAll()
    {
        Muted = false;
        int IND = 0;
        foreach (Sound s in sounds)
        {
            s.volume = volumes[IND];
            s.source.volume = volumes[IND];
            IND++;
        }

    }
}
