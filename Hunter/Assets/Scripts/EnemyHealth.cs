using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
   [SerializeField] int health = 3;

    public void DecreaseHealth()
    {
        health--;

        if (health <= 0)
            Death();
    }

    private void Death()
    {
        StartCoroutine(ExpiryColor.ExpirySpriteColor(GetComponent<SpriteRenderer>()));
        DisableCollider();

    }

    void DisableCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}
