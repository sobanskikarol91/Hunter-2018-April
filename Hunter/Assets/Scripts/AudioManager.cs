using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager ins;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Clip winSnd;
    [SerializeField] Clip loseSnd;

    private void Awake()
    {
        ins = this;
    }

    public void PlayWinSnd()
    {
        audioSource.Play(winSnd);
    }

    public void PlayLoseSnd()
    {
        audioSource.Play(loseSnd);
    }
}
