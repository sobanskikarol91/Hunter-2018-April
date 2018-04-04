using UnityEngine;


public class BowEventManager : MonoBehaviour
{
    public static BowEventManager instance; 
    public delegate void State();
    public static event State OnGrabArrow = delegate { };
    public static event State OnDraggingArrow = delegate { };
    public static event State OnReleaseArrow = delegate { };

    enum BowState { OnGrabArrow, OnDraggingArrow, OnReleaseArrow, OnIdle }
    BowState state = BowState.OnIdle;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        switch (state)
        {
            case BowState.OnIdle:
                    break;
            case BowState.OnDraggingArrow:
                {
                    OnDraggingArrow();
                    break;
                }
            case BowState.OnGrabArrow:
                {
                    OnGrabArrow();
                    ChangeStateToOnDraggingArrow();
                    break;
                }
            case BowState.OnReleaseArrow:
                {
                    OnReleaseArrow();
                    ChangeStateToOnIdle();
                    break;
                }
        }
    }

    public void ChangeStateToOnDraggingArrow()
    {
        state = BowState.OnDraggingArrow;
    }

    public void ChangeStateToOnGrabbingArrow()
    {
        state = BowState.OnGrabArrow;
    }

    public void ChangeStateToOnReleaseArrow()
    {
        state = BowState.OnReleaseArrow;
    }

    public void ChangeStateToOnIdle()
    {
        state = BowState.OnIdle;
    }
}
