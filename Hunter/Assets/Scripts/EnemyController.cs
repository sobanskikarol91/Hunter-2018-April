using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    EnemyHealth enemyHealth;

    private void Start()
    {
        GameManager.instance.RegisterEnemy(this);
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != TagManager.arrow) return;
        enemyHealth.DecreaseHealth();
        GameManager.instance.UnregisterEnemy(this);
    }
}
