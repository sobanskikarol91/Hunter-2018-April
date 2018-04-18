using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public  class Obstacle : MonoBehaviour
{
    protected AudioSource audioSource;
    protected Animator animator;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagManager.arrow)
            CollisionWithArrow(collision);
    }

    protected virtual void CollisionWithArrow(Collision2D collision)
    {
        audioSource.Play();
        animator.SetTrigger("ArrowHit");
        if (collision.contacts.Length > 0)
            ObjectPoolerManager.instance.SpawnFromPool("TrampolineParticle", collision.contacts[0].point, Quaternion.identity);
    }
}
