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

    private Vector3 startPosition;
    private Coroutine rotateCorutine;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        AddEvents();
        GameManager.instance.RegisterArrow(this);
    }

    private void OnBecameInvisible()
    {
        Invoke("DisableArrow", onBecomeInvisibleTime);
    }

    private void OnDisable()
    {
        GameManager.instance.UnregisterArrow(this);
        DeleteEvents();
    }

    #region +/- Events
    void AddEvents()
    {
        BowEventManager.shooting.OnEnter += GrabArrow;
        BowEventManager.shooting.OnExecute += DragArrow;
        BowEventManager.shooting.OnExit += ReleaseArrow;
        BowEventManager.startGame.OnEnter += DisableArrow;
    }

    void DeleteEvents()
    {
        BowEventManager.shooting.OnEnter -= GrabArrow;
        BowEventManager.shooting.OnExecute -= DragArrow;
        BowEventManager.shooting.OnExit -= ReleaseArrow;
        BowEventManager.startGame.OnEnter -= DisableArrow;
    }
    #endregion
    IEnumerator FlyingRotate()
    {
        while (true)
        {
            Vector3 dir = Rb.velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrowSprite.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return null;
        }
    }

    void DisableColliders()
    {
        missCollider.enabled = hitCollider.enabled = false;
    }

    void EnableColliders()
    {
        missCollider.enabled = hitCollider.enabled = true;
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
        Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 startMousePos = Camera.main.ScreenToWorldPoint(BowEventManager.StartPressPosition);
        Vector2 directionFromMouseToBow = currentMousePos - startMousePos;
        directionFromMouseToBow = directionFromMouseToBow.ClampMagnitudeMinMax(startPosition.magnitude, bowController.MaxTenseDistance);

        if (directionFromMouseToBow != Vector2.zero)
            transform.position = directionFromMouseToBow;

      HUDManager.instance.SetTenseMeter((directionFromMouseToBow.magnitude - startPosition.magnitude) / (bowController.MaxTenseDistance - startPosition.magnitude));
    }

    public void ReleaseArrow()
    {
        BowEventManager.shooting.OnEnter -= GrabArrow;
        BowEventManager.shooting.OnExecute -= DragArrow;
        BowEventManager.shooting.OnExit -= ReleaseArrow;

        EnableColliders();
        Rb.isKinematic = false;
        transform.SetParent(null);
        rotateCorutine = StartCoroutine(FlyingRotate());
        audioSource.Play(flyingClip);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.transform.tag;
        if (tag == TagManager.obstacle && collision.contacts.Length > 0)
        {
            ScoreManager.instance.AddScore(collision);
            ArrowStickInObstacle(collision);
            //StartCoroutine(ExpiryColor.ExpirySpriteColor(arrowSprite.GetComponent<SpriteRenderer>()));
            IArrowHitEffect[] arrowsHitEffects = collision.gameObject.GetComponents<IArrowHitEffect>();
            arrowsHitEffects.ForEach(t => t.ArrowHitEffect());
        }
        else if (tag == TagManager.obstacle && collision.contacts.Length == 0)
            Debug.LogWarning("contact length 0");
        else if (tag == TagManager.block)
            ArrowStickInObstacle(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == TagManager.bow || collision.gameObject.tag == TagManager.arrow) return;
        //MissObstacle();
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
        Invoke("DisableColliders", 0.1f);
    }

    void RestoreSpriteColor()
    {
        arrowSprite.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void PrepareObjectToSpawn()
    {
        hitParicle.SetActive(false);
        trailParticle.Stop();
        DisableColliders();
        startPosition = transform.position;
        StopAllCoroutines();
        Rb.bodyType = RigidbodyType2D.Kinematic;
        RestoreSpriteColor();
        arrowSprite.localRotation = Quaternion.Euler(Vector3.zero);
        Rb.velocity = Vector3.zero;
        animator.SetBool("vibrations", false);
    }

    void DisableArrow()
    {
        gameObject.SetActive(false);
    }
}