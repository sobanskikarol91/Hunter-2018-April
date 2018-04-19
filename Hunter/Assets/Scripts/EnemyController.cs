using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour, IObjectPooler, IArrowHitEffect
{
    EnemyHealth enemyHealth;

    private void Start()
    {
        GameManager.instance.RegisterEnemy(this);
        enemyHealth = GetComponent<EnemyHealth>();
    }

    public void PrepareObjectToSpawn()
    {
        GameManager.instance.RegisterEnemy(this);
    }

    public void ArrowHitEffect()
    {
        enemyHealth.DecreaseHealth();
        GameManager.instance.UnregisterEnemy(this);
    }
}
