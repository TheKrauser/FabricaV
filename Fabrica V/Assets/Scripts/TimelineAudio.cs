using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimelineAudio : MonoBehaviour
{
    public SoundClass[] sfxAmbience, sfxSoundtrack;

    private void Awake()
    {
        foreach (SoundClass s in sfxAmbience)
        {
            s.audioS = gameObject.AddComponent<AudioSource>();
            s.audioS.clip = s.clip;
            s.audioS.volume = s.volume;
            s.audioS.pitch = s.pitch;
            s.audioS.loop = s.loop;
            s.audioS.outputAudioMixerGroup = s.mixerGroup;
        }

        foreach (SoundClass s in sfxSoundtrack)
        {
            s.audioS = gameObject.AddComponent<AudioSource>();
            s.audioS.clip = s.clip;
            s.audioS.volume = s.volume;
            s.audioS.pitch = s.pitch;
            s.audioS.loop = s.loop;
            s.audioS.outputAudioMixerGroup = s.mixerGroup;
        }
    }

    public void PlaySoundtrack(string soundName)
    {
        SoundClass s = Array.Find(sfxSoundtrack, sound => sound.name == soundName);
        if (s == null)
            return;

        s.audioS.Play();
    }

    public void StopSoundtrack(string soundName)
    {
        SoundClass s = Array.Find(sfxSoundtrack, sound => sound.name == soundName);
        if (s == null)
            return;

        s.audioS.Stop();
    }

    public void PlayAmbience(string soundName)
    {
        SoundClass s = Array.Find(sfxAmbience, sound => sound.name == soundName);
        if (s == null)
            return;

        s.audioS.Play();
    }

    public void StopAmbience(string soundName)
    {
        SoundClass s = Array.Find(sfxAmbience, sound => sound.name == soundName);
        if (s == null)
            return;

        StartCoroutine(FadeSound(s, 0.05f));
    }

    private IEnumerator FadeSound(SoundClass sound, float time)
    {
        float value = sound.audioS.volume * 0.01f;

        while(sound.audioS.volume > 0)
        {
            yield return new WaitForSecondsRealtime(time);
            sound.audioS.volume -= value;
        }

        if (sound.audioS.volume <= 0)
        {
            sound.audioS.volume = 0;
        }

        sound.audioS.Stop();
    }
}
