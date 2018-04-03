using UnityEngine;

public class Arrow : MonoBehaviour, IBow
{
    public Rigidbody2D Rb { get; private set; }

    [SerializeField] Collider2D collider;
    private float originDistanceToBow;
    [HideInInspector] public BowController bowController;

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
        collider.enabled = false;
    }

    public void GrabArrow()
    {
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
        BowEventManager.OnGrabArrow -= GrabArrow;
        BowEventManager.OnDraggingArrow -= DragArrow;
        Rb.isKinematic = false;
    }
}