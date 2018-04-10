using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public static Transform Player { get; private set; }
    [SerializeField] Quiver quiver;


    List<EnemyController> enemies = new List<EnemyController>();
    List<Arrow> activeArrows = new List<Arrow>();
    
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

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Bow").transform;
    }

    public void GameOver()
    {
        AudioManager.instance.PlayLoseSnd();
        MenuManager.instance.SwitchToMenu(MENU.GameOver);
    }

    public void LvlCompleted()
    {
        AudioManager.instance.PlayWinSnd();
        MenuManager.instance.SwitchToMenu(MENU.GameOver);
    }
}
