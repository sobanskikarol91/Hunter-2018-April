using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player;
    public Transform Player { get { return player; } }

    List<EnemyController> enemies = new List<EnemyController>();
    List<Arrow> activeArrows = new List<Arrow>();
    [SerializeField] Quiver quiver;

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region Observer patterns
    public void RegisterEnemy(EnemyController enemy) { enemies.Add(enemy); }
    public void RegisterArrow(Arrow arrow) { activeArrows.Add(arrow); }

    public void UnregisterArrow(Arrow arrow)
    {
        activeArrows.Remove(arrow);

        if (activeArrows.Count == 0 && quiver.IsEmpty())
            GameOver();
    }
    
    public void UnregisterEnemy(EnemyController enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
            Invoke("LvlCompleted", 0.5f);
    }
    #endregion


    public void GameOver()
    {
        AudioManager.ins.PlayLoseSnd();
        MenuManager.instance.SwitchToGameOver();
    }

    public void LvlCompleted()
    {
        AudioManager.ins.PlayWinSnd();
        MenuManager.instance.SwitchToGameOver();
    }
}
