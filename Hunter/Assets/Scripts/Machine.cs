using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Machine : MonoBehaviour
{
    AudioSource audioSource;
    Animator animator;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != TagManager.arrow) return;

        audioSource.Play();
        animator.SetTrigger("ArrowHit");
    }
}
