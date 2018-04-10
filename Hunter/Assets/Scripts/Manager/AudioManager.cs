using UnityEngine;
using System.Collections;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Clip winSnd;
    [SerializeField] Clip loseSnd;

    public void PlayWinSnd()
    {
        audioSource.Play(winSnd);
    }

    public void PlayLoseSnd()
    {
        audioSource.Play(loseSnd);
    }
}
