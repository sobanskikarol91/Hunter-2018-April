using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour, IBow
{
    public Rigidbody2D Rb { get; private set; }
    [SerializeField] Transform arrowSprite;
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
        transform.SetParent(null);
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            Vector3 dir = Rb.velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrowSprite.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return null;
        }
    }
}