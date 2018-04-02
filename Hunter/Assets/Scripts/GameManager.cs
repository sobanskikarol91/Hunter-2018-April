using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    BowController bow;

    public delegate void State();
    public static event State StartGame;

    private void Start()
    {
        //TODO: TEST
        Invoke("Play", 0.1f);
    }

    void Play()
    {
        //TODO: TEST
        StartGame();
    }

    void Update()
    {
        //TODO: TEST
        if (Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadScene(0);
    }
}
