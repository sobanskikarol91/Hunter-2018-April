using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour, IBow
{
    public Rigidbody2D Rb { get; private set; }

    [SerializeField] Transform arrowSprite;
    [SerializeField] Collider2D hitCollider;
    [SerializeField] Collider2D missCollider;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator animator;
    [SerializeField] Clip swishClip;
    [SerializeField] Clip hitClip;

    [HideInInspector] public BowController bowController;

    private float originDistanceToBow;
    private Coroutine rotateCorutine;



    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        EnableColliders(false);
    }

    private void Start()
    {
        BowEventManager.OnGrabArrow += GrabArrow;
        BowEventManager.OnDraggingArrow += DragArrow;
        BowEventManager.OnReleaseArrow += ReleaseArrow;
        originDistanceToBow = transform.position.magnitude;
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

    void EnableColliders(bool state)
    {
        missCollider.enabled = hitCollider.enabled = state;
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

        EnableColliders(true);
        Rb.isKinematic = false;
        transform.SetParent(null);
        rotateCorutine = StartCoroutine(Rotate());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == TagManager.obstacle)
        {
            EnableColliders(false);
            audioSource.Play(hitClip);
            StopCoroutine(rotateCorutine);
            Rb.bodyType = RigidbodyType2D.Static;
            animator.SetTrigger("vibrations");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagManager.bow || collision.gameObject.tag == TagManager.arrow) return;
        audioSource.Play(swishClip);
    }
}