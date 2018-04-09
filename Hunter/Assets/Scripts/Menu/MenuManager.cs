using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] GameObject gameOver;
    [SerializeField] float gameOverShowDuration = 2f;
    private void Awake()
    {
        instance = this;
        DisableMenus();
    }

    void DisableMenus()
    {
        gameOver.SetActive(false);
    }

    public void SwitchToGameOver()
    {
        Invoke("GameOver", gameOverShowDuration);
    }

    void GameOver()
    {
        gameOver.SetActive(true);
    }
}
