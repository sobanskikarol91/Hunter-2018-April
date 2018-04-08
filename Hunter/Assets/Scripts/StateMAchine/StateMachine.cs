using UnityEngine;
using UnityEngine.UI;

public class StateMachine 
{
    public State CurrentState { get; private set; }
    public State PreviousState { get; private set; }

    public StateMachine()
    {
        CurrentState = PreviousState = new State();
    }

    public void ChangeState(State nextState)
    {
       // Debug.Log(nextState.name);
        PreviousState = CurrentState;
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }

    public void SwitchToPreviousState()
    {
        if (PreviousState == null) return;
        ChangeState(PreviousState);
    }

    public void ExecuteCurrentState()
    {
        if (CurrentState == null) return;
        CurrentState.Execute();
    }
}
