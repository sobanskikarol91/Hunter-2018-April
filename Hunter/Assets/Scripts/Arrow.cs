using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour, IBow
{
    public Rigidbody2D Rb { get; private set; }
    string obstacleTag = "Obstacle";
    [SerializeField] Transform arrowSprite;
    [SerializeField] Collider2D arrowCollider;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator animator;
    private float originDistanceToBow;
    [HideInInspector] public BowController bowController;
    Coroutine rotateCorutine;


    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        arrowCollider.enabled = false;
    }

    private void Start()
    {
        BowEventManager.OnGrabArrow += GrabArrow;
        BowEventManager.OnDraggingArrow += DragArrow;
        BowEventManager.OnReleaseArrow += ReleaseArrow;
        originDistanceToBow = transform.position.magnitude;
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
        BowEventManager.OnReleaseArrow -= ReleaseArrow;

        arrowCollider.enabled = true;
        Rb.isKinematic = false;
        transform.SetParent(null);
        rotateCorutine = StartCoroutine(Rotate());
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == obstacleTag)
        {
            audioSource.Play();
            StopCoroutine(rotateCorutine);
            Rb.bodyType = RigidbodyType2D.Static;
            animator.SetTrigger("vibrations");
        }
    }
}