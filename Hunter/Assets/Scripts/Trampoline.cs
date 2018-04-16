using UnityEngine;
using System.Collections;

public class Trampoline : MonoBehaviour
{
    AudioSource audioSource;
    Animator animator;
    [SerializeField] GameObject particles;

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
            if (collision.contacts.Length > 0)
                ObjectPoolerManager.instance.SpawnFromPool("TrampolineParticle", collision.contacts[0].point, Quaternion.identity);
        }
    }
}
