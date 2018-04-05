using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public delegate void State();
    public static event State StartGame;
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        //TODO: TEST
        Invoke("Play", 0.1f);
    }

    void Play()
    {
        //TODO: TEST
        StartGame();
    }
}
