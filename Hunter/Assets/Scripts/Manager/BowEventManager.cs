using UnityEngine;

public class BowEventManager : Singleton<BowEventManager>
{
    public static State idle = new State("Idle");
    public static State spawnArrow = new State("Spawn");
    public static State shooting = new State("Shoot");
    public static State waitingForFallingArrow = new State("FallingArrow");
    public static State menu = new State("Menu");
    [SerializeField] public Quiver quiver;

    private StateMachine stateMachine = new StateMachine();

    public static Vector2 StartPressPosition { get; private set; }

    private void Start()
    {
        spawnArrow.OnExecute += WaitForPlayerFire;
        shooting.OnExecute += WaitForPlayerRelease;
    }

    private void Update()
    {
        stateMachine.ExecuteCurrentState();
    }

    public void StartGame()
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

    public void ChangeStateToMain()
    {
        stateMachine.ChangeState(menu);
    }

    public void ChangeStateToOnSpawning()
    {
        Invoke("Spawn", 0.3f);
    }

    public bool IsGameOver()
    {
        return stateMachine.CurrentState == menu;
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

    void WaitForPlayerFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartPressPosition = Input.mousePosition;
            ChangeStateToShooting();
        }
    }

    void WaitForPlayerRelease()
    {
        if (Input.GetMouseButtonUp(0))
            FindArrowInQuiver();
    }
}
