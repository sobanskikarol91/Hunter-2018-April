using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IObjectPooler
{
    [SerializeField] int health = 3;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DecreaseHealth()
    {
        health--;

        if (health <= 0)
            Death();
    }

    private void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(ExpiryColor.ExpirySpriteColor(spriteRenderer));
        EnableCollider(false);
    }

    void EnableCollider(bool state)
    {
        GetComponent<Collider2D>().enabled = state;
    }

    public void PrepareObjectToSpawn()
    {
        spriteRenderer.color = Color.white;
        EnableCollider(true);
    }
}
