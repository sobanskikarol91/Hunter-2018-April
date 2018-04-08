using UnityEngine;
public class State
{
   public string name = "No name";
    public delegate void StateDelegate();
    public event StateDelegate OnEnter = delegate { };
    public event StateDelegate OnExecute = delegate { };
    public event StateDelegate OnExit = delegate { };

    public State()
    {

    }

    public State(string name)
    {
        this.name = name;
    }
    public void Enter()
    {
        OnEnter();
    }

    public void Execute()
    {
        OnExecute();
    }

    public void Exit()
    {
        OnExit();
    }
}
