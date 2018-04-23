using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Quiver quiver;

    List<EnemyController> enemies = new List<EnemyController>();
    List<Arrow> activeArrows = new List<Arrow>();

    #region Observer patterns
    public void RegisterEnemy(EnemyController enemy) { enemies.Add(enemy); }
    public void RegisterArrow(Arrow arrow) { activeArrows.Add(arrow); }

    public void UnregisterArrow(Arrow arrow)
    {
        if (!activeArrows.Contains(arrow)) return;

        activeArrows.Remove(arrow);

        if (activeArrows.Count == 0 && quiver.IsEmpty() && !BowEventManager.instance.IsGameOver())
            StartCoroutine(LvlCompleted(false));
    }

    public void UnregisterEnemy(EnemyController enemy)
    {
        if (!enemies.Contains(enemy)) return;

        enemies.Remove(enemy);

        if (enemies.Count == 0 && !BowEventManager.instance.IsGameOver())
            StartCoroutine(LvlCompleted(true));
    }
    #endregion

    IEnumerator LvlCompleted(bool isCompleted)
    {
        yield return new WaitForSeconds(0.5f);
        if (isCompleted) AudioManager.instance.PlayWinSnd();
        else AudioManager.instance.PlayLoseSnd();
        MenuManager.instance.SwitchToMenu(MENU.GameOver);
        BowEventManager.instance.ChangeStateToMain();
    }

    public void PlayerGiveUp()
    {
        StartCoroutine(LvlCompleted(false));
    }
}
