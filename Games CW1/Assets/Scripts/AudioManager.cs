using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * PlayerPrefs.GetFloat("sfxVolume");
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void updateVolume()
    {
        foreach(Sound s in sounds)
        {
            s.source.volume = s.volume * PlayerPrefs.GetFloat("sfxVolume");
        }
    }

    public void play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Play();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
