using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour
{
    AudioSource audioSource;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagManager.arrow)
        {
            animator.SetTrigger("Bouncy");
            audioSource.Play();
        }
    }
}
