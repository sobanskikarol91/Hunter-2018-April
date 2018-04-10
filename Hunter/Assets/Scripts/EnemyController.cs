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

    public void ArrowHit()
    {
        enemyHealth.DecreaseHealth();
        GameManager.instance.UnregisterEnemy(this);
    }
}
