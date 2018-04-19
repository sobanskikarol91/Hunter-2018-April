using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour, IBow, IObjectPooler
{
    public Rigidbody2D Rb { get; private set; }

    [SerializeField] Transform massCenter;
    [SerializeField] Transform arrowSprite;
    [SerializeField] Collider2D hitCollider;
    [SerializeField] Collider2D missCollider;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator animator;
    [SerializeField] Clip hitClip;
    [SerializeField] Clip flyingClip;
    [SerializeField] ParticleSystem trailParticle;
    [SerializeField] GameObject hitParicle;
    [SerializeField] float onBecomeInvisibleTime = 0.5f;

    [HideInInspector] public BowController bowController;

    private float originDistanceToBow;
    private Coroutine rotateCorutine;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    void AddEvents()
    {
        BowEventManager.shooting.OnEnter += GrabArrow;
        BowEventManager.shooting.OnExecute += DragArrow;
        BowEventManager.shooting.OnExit += ReleaseArrow;
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
        trailParticle.Play();
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
        BowEventManager.shooting.OnEnter -= GrabArrow;
        BowEventManager.shooting.OnExecute -= DragArrow;
        BowEventManager.shooting.OnExit -= ReleaseArrow;

        EnableColliders(true);
        Rb.isKinematic = false;
        transform.SetParent(null);
        rotateCorutine = StartCoroutine(Rotate());
        audioSource.Play(flyingClip);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.transform.tag;
        if (tag == TagManager.obstacle && collision.contacts.Length >0)
        {
            ScoreManager.instance.AddScore(collision);
            ArrowStickInObstacle(collision);
            //StartCoroutine(ExpiryColor.ExpirySpriteColor(arrowSprite.GetComponent<SpriteRenderer>()));
            IArrowHitEffect[] arrowsHitEffects = collision.gameObject.GetComponents<IArrowHitEffect>();
            arrowsHitEffects.ForEach(t => t.ArrowHitEffect());
        }
        else if (tag == TagManager.obstacle && collision.contacts.Length == 0)
            Debug.LogWarning("contact length 0");
        else if(tag == TagManager.block)
            ArrowStickInObstacle(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagManager.bow || collision.gameObject.tag == TagManager.arrow) return;
        MissObstacle();
    }

    void MissObstacle()
    {
        FloatingTextManager.instance.ShowFloatingText(FLOATING_TXT.Miss, transform.position);
    }

    void ArrowStickInObstacle(Collision2D collision)
    {
        StopCoroutine(rotateCorutine);
        animator.SetBool("vibrations", true);
        gameObject.transform.SetParent(collision.transform);
        Rb.bodyType = RigidbodyType2D.Static;

        audioSource.Play(hitClip);
        //FloatingTextManager.instance.ShowFloatingText(FLOATING_TXT.Hit, transform.position);
        hitParicle.SetActive(true);
     //   EnableColliders(false);
    }

    void RestoreSpriteColor()
    {
        arrowSprite.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void PrepareObjectToSpawn()
    {
        hitParicle.SetActive(false);
        trailParticle.Stop();
        EnableColliders(false);
        originDistanceToBow = transform.position.magnitude;
        AddEvents();
        StopAllCoroutines();
        Rb.bodyType = RigidbodyType2D.Kinematic;
        RestoreSpriteColor();
        arrowSprite.localRotation = Quaternion.Euler(Vector3.zero);
        Rb.velocity = Vector3.zero;
        animator.SetBool("vibrations", false);
    }

    void OnEnable()
    {
        GameManager.instance.RegisterArrow(this);
    }

    private void OnBecameInvisible()
    {
        Invoke("DisableArrowAfterTime", onBecomeInvisibleTime);
    }

    private void OnDisable()
    {
        GameManager.instance.UnregisterArrow(this);
    }

    void DisableArrowAfterTime()
    {
        gameObject.SetActive(false);
    }
}