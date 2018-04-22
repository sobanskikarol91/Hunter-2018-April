using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour, IObjectPooler, IArrowHitEffect
{
    EnemyHealth enemyHealth;

    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        GameManager.instance.RegisterEnemy(this);
    }

    private void OnDisable()
    {
        GameManager.instance.UnregisterEnemy(this);
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
