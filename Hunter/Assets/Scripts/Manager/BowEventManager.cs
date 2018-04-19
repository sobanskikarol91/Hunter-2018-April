using UnityEngine;

public class BowEventManager : Singleton<BowEventManager>
{
    public static State idle = new State("Idle");
    public static State spawnArrow = new State("Spawn");
    public static State shooting = new State("Shoot");
    public static State waitingForFallingArrow = new State("Shoot");

    [SerializeField] public Quiver quiver;

    private StateMachine stateMachine = new StateMachine();

    public static Vector2 StartPressPosition { get; private set; }

    private void Start()
    {
        Invoke("StartGame", .1f);
    }

    private void Update()
    {
        stateMachine.ExecuteCurrentState();

        if (Input.GetMouseButtonDown(0))
        {
            StartPressPosition = Input.mousePosition;
            ChangeStateToShooting();
        }
        else if (Input.GetMouseButtonUp(0))
            FindArrowInQuiver();
    }

    void StartGame()
    {
        stateMachine.ChangeState(spawnArrow);
    }

    public void ChangeStateToShooting()
    {
        if (stateMachine.CurrentState == spawnArrow)
            stateMachine.ChangeState(shooting);
    }

    public void ChangeStateToIdle()
    {
        stateMachine.ChangeState(idle);
    }

    public void ChangeStateToOnSpawning()
    {
        Invoke("Spawn", 0.2f);
    }

    void Spawn()
    {
        stateMachine.ChangeState(spawnArrow);
    }

    public void FindArrowInQuiver()
    {
        ChangeStateToIdle();
        if (quiver.LeftArrows > 0)
            ChangeStateToOnSpawning();
        else
            stateMachine.ChangeState(waitingForFallingArrow);
    }
}
