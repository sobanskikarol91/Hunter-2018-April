using UnityEngine;

public class FloatingText : MonoBehaviour, IObjectPooler
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PrepareObjectToSpawn()
    {
        animator.Play("ShowFloatingText", -1, 0f);

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.Play();
    }
}