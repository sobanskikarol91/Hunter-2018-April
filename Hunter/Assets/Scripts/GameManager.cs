using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] Transform player;
    public Transform Player { get { return player; } }

    List<EnemyController> enemies = new List<EnemyController>();

    public void RegisterEnemy(EnemyController enemy)
    {
        enemies.Add(enemy);
    }

    public void UnregisterEnemy(EnemyController enemy)
    {
        enemies.Remove(enemy);

        if (enemies.Count == 0)
            GameOver();
    }

    private void Awake()
    {
        instance = this;
    }

    public void GameOver()
    {
        Debug.Log("game Over");
    }
}
