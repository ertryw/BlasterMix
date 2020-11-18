using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundMenager
{
    
   public static void Play(AudioClip clip, float volume)
    {
        GameObject soundGameObject = new GameObject("Soundgood");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        soundGameObject.AddComponent<destroy>();
        audioSource.volume = volume;
        audioSource.PlayOneShot(clip);
    }

    public static GameObject PlayLoop(AudioClip clip, float volume)
    {
        GameObject soundGameObject = new GameObject("SoundLaser");
        soundGameObject.transform.tag = "SoundLaser";
       AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.PlayOneShot(clip);
        return soundGameObject;
    }

}
