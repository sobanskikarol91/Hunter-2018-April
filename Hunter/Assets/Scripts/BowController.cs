using UnityEngine;
using System.Collections;
using System;

public class BowController : MonoBehaviour, IBow
{
    public Transform ArrowSpawnPoint { get { return arrowSpawnPoint; } }
    public float MaxTenseDistance { get { return maxTenseDistance; } }
    public Arrow CurrentUsingArrow { get; private set; }

    [SerializeField] SpringJoint2D joint;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float maxTenseDistance = 1f;
    [SerializeField] Transform arrowSpawnPoint;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Clip tenseSnd;
    [SerializeField] Clip shotSnd;

    #region Add/Delete Events
    private void OnEnable()
    {
        BowEventManager.shooting.OnEnter += GrabArrow;
        BowEventManager.shooting.OnExecute += DragArrow;
        BowEventManager.shooting.OnExit += ReleaseArrow;
        BowEventManager.spawnArrow.OnEnter += SpawnArrow;
    }

    private void OnDisable()
    {
        BowEventManager.shooting.OnEnter -= GrabArrow;
        BowEventManager.shooting.OnExecute -= DragArrow;
        BowEventManager.shooting.OnExit -= ReleaseArrow;
        BowEventManager.spawnArrow.OnEnter -= SpawnArrow;
    }
    #endregion

    public void DiscotectedArrowFromJoint()
    {
        joint.connectedBody = null;
    }

    void SpawnArrow()
    {
        GameObject arrowGo = ObjectPoolerManager.instance.SpawnFromPool("Arrow", arrowSpawnPoint.position, transform.rotation);
        CurrentUsingArrow = arrowGo.GetComponent<Arrow>();
        joint.connectedBody = CurrentUsingArrow.Rb;
        CurrentUsingArrow.bowController = this;
        CurrentUsingArrow.transform.SetParent(arrowSpawnPoint);
    }

    void RotateBow()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 direction = BowEventManager.StartPressPosition - mousePos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        HUDManager.instance.SetAngleTxt(angle);
    }


    IEnumerator CheckPossibilityToUnhookArrow()
    {
        Vector2 previousVelocity = Vector2.zero;
        Func<Vector2> ArrowVelocity = () => CurrentUsingArrow.Rb.velocity;

        while (ArrowVelocity().magnitude >= previousVelocity.magnitude)
        {
            previousVelocity = ArrowVelocity();
            yield return null;
        }

        CurrentUsingArrow.Rb.velocity = previousVelocity;
        DiscotectedArrowFromJoint();
    }

    private void UpdateJointAnchorToSpawnArrowPoint()
    {
        joint.anchor = arrowSpawnPoint.localPosition;
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
    }
}