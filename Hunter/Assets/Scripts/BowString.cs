using UnityEngine;
using System.Linq;


public class BowString : MonoBehaviour, IBow
{
    [SerializeField] Transform[] bowstringHooks;
    [SerializeField] BowController bowController;
    [SerializeField] LineRenderer lineRenderer;

    private void OnEnable()
    {
        BowEventManager.spawnArrow.OnEnter += UpdateLineRenderer;
        BowEventManager.shooting.OnEnter += GrabArrow;
        BowEventManager.shooting.OnExecute += DragArrow;
        BowEventManager.shooting.OnExit += ReleaseArrow;
    }

    private void OnDisable()
    {
        BowEventManager.spawnArrow.OnEnter -= UpdateLineRenderer;
        BowEventManager.shooting.OnEnter -= GrabArrow;
        BowEventManager.shooting.OnExecute -= DragArrow;
        BowEventManager.shooting.OnExit -= ReleaseArrow;
    }

    void UpdateLineRenderer()
    {
        lineRenderer.positionCount = bowstringHooks.Length;
        lineRenderer.SetPositions(bowstringHooks.Select(t => t.position).ToArray());
    }

    void SetBowstringPositionRelativeToArrow()
    {
        bowstringHooks[1].position = bowController.CurrentUsingArrow.transform.position;
    }

    void ResetArrowHolderSpotPosition()
    {
        bowstringHooks[1].position = (bowstringHooks.First().position + bowstringHooks.Last().position) / 2;
    }

    public void GrabArrow()
    {

    }

    public void DragArrow()
    {
        SetBowstringPositionRelativeToArrow();
        UpdateLineRenderer();
    }

    public void ReleaseArrow()
    {
        //TODO: smooth bowstring return to orginal position
        ResetArrowHolderSpotPosition();
        UpdateLineRenderer();
    }
}
