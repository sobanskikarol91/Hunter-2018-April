using UnityEngine;

public class BowEventManager : Singleton<BowEventManager>
{
    public static State idle = new State("Idle");
    public static State spawnArrow = new State("Spawn");
    public static State shooting = new State("Shoot");
    public static State waitingForFallingArrow = new State("Shoot");

    [SerializeField] public Quiver quiver;

    private StateMachine stateMachine = new StateMachine();

    private void Start()
    {
        Invoke("StartGame", .1f);
    }

    void StartGame()
    {
        stateMachine.ChangeState(spawnArrow);
    }

    private void Update()
    {
        stateMachine.ExecuteCurrentState();
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
