using UnityEngine;

public class Arrow : MonoBehaviour, IBow
{
    public Rigidbody2D Rb { get; private set; }

    private float originDistanceToBow;
    public BowController bowController;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        BowEventManager.OnGrabArrow += GrabArrow;
        BowEventManager.OnDraggingArrow += DragArrow;
        BowEventManager.OnReleaseArrow += ReleaseArrow;

        originDistanceToBow = transform.position.magnitude;
    }

    private void OnMouseDown()
    {
        BowEventManager.instance.ChangeStateToOnDraggingArrow();
    }

    private void OnMouseUp()
    {
        BowEventManager.instance.ChangeStateToOnReleaseArrow();
        ReleaseArrow();
    }

    public void GrabArrow()
    {
        Rb.isKinematic = true;
    }

    public void DragArrow()
    {
        SetArrowPositionRelativeToMouse();
    }

    void SetArrowPositionRelativeToMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionFromMouseToBow = mousePos - (Vector2)bowController.transform.position;
        directionFromMouseToBow = directionFromMouseToBow.ClampMagnitudeMinMax(originDistanceToBow, bowController.MaxTenseDistance);

        transform.position = directionFromMouseToBow;
    }

    public void ReleaseArrow()
    {
        Rb.isKinematic = false;
    }
}