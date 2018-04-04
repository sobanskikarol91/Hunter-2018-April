using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Clip
{
    public AudioClip clip;
    public AudioMixerGroup group;
}


public static class ExtensionClip
{
    public static void Play(this AudioSource audioSource, Clip clip)
    {
        audioSource.clip = clip.clip;
        audioSource.outputAudioMixerGroup = clip.group;
        audioSource.Play();
    }
}
