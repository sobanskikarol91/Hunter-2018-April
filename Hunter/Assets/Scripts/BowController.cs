using UnityEngine;
using System.Collections;
using System;

public class BowController : MonoBehaviour, IBow
{
    public Transform ArrowSpawnPoint { get { return arrowSpawnPoint; } }
    public float MaxTenseDistance { get { return maxTenseDistance; } }
    public Arrow CurrentUsingArrow { get; private set; }

    [SerializeField] SpringJoint2D joint;
    [SerializeField] float minDistanceToUnhookArrow = 0.1f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float maxTenseDistance = 1f;
    [SerializeField] Transform arrowSpawnPoint;
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Clip tenseSnd;
    [SerializeField] Clip shotSnd;

    private void Start()
    {
        GameManager.StartGame += SpawnArrow;
        BowEventManager.OnGrabArrow += GrabArrow;
        BowEventManager.OnDraggingArrow += DragArrow;
        BowEventManager.OnReleaseArrow += ReleaseArrow;
    }

    public void DiscotectedArrowFromJoint()
    {
        joint.connectedBody = null;
    }

    void SpawnArrow()
    {
        CurrentUsingArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, transform.rotation).GetComponent<Arrow>();
        joint.connectedBody = CurrentUsingArrow.Rb;
        CurrentUsingArrow.bowController = this;
        CurrentUsingArrow.transform.SetParent(arrowSpawnPoint);
    }

    void RotateBow()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = transform.position - mousePos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    IEnumerator CheckPossibilityToUnhookArrow()
    {
        Func<Vector2> CurrentArrowDirectionToBow = () => { return arrowSpawnPoint.position - CurrentUsingArrow.transform.position; };
        Vector2 lastDirection = CurrentArrowDirectionToBow();

        while (Vector2.Dot(lastDirection, CurrentArrowDirectionToBow()) > minDistanceToUnhookArrow)
        {
            lastDirection = CurrentArrowDirectionToBow();
            yield return null;
        }

        DiscotectedArrowFromJoint();
    }   

    private void UpdateJointAnchorToSpawnArrowPoint()
    {
        joint.anchor = arrowSpawnPoint.localPosition;
    }

    private void OnMouseDown()
    {
        BowEventManager.instance.ChangeStateToOnGrabbingArrow();
    }

    private void OnMouseUp()
    {
        BowEventManager.instance.ChangeStateToOnReleaseArrow();
    }

    public void GrabArrow()
    {
        audioSource.Play(tenseSnd);
    }

    public void DragArrow()
    {
        RotateBow();
        UpdateJointAnchorToSpawnArrowPoint();
    }

    public void ReleaseArrow()
    {
        audioSource.Play(shotSnd);
        StartCoroutine(CheckPossibilityToUnhookArrow());
        // TEST
        Invoke("SpawnArrow", 0.1f);
    }
}
