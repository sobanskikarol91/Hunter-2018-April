using System.Collections;
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
            StartCoroutine(LvlCompleted(false));
    }

    public void UnregisterEnemy(EnemyController enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
            StartCoroutine(LvlCompleted(true));
    }
    #endregion

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Bow").transform;
    }

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
