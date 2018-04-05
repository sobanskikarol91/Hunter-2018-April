using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int health = 1;

    void DecreaseHealth()
    {
        health--;

        if (health <= 0)
            Death();
    }

    private void Death()
    {
        StartCoroutine(ExpiryColor.ExpirySpriteColor(GetComponent<SpriteRenderer>()));
        DisableCollider();
        GetComponent<EnemyMovement>().enabled = false;
    }

    void DisableCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != TagManager.arrow) return;
        DecreaseHealth();
    }
}
