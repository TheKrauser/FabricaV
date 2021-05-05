using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public SoundClass[] sfxSounds, sfxAmbience, sfxSoundtrack;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (SoundClass s in sfxSounds)
        {
            s.audioS = gameObject.AddComponent<AudioSource>();
            s.audioS.clip = s.clip;
            s.audioS.volume = s.volume;
            s.audioS.pitch = s.pitch;
            s.audioS.loop = s.loop;
        }
    }

    public void PlaySoundEffect(string soundName)
    {
        SoundClass s = Array.Find(sfxSounds, sound => sound.name == soundName);
        if (s == null)
            return;

        s.audioS.Play();
    }
}
