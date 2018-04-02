using UnityEngine;
using System.Linq;


public class BowString : MonoBehaviour, IBow
{
    [SerializeField] Transform[] bowstringHooks;
    [SerializeField] BowController bowController;
    [SerializeField] LineRenderer lineRenderer;

    private void Start()
    {
        UpdateLineRenderer();

        GameManager.StartGame += UpdateLineRenderer;
        BowEventManager.OnGrabArrow += GrabArrow;
        BowEventManager.OnDraggingArrow += DragArrow;
        BowEventManager.OnReleaseArrow += ReleaseArrow;
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
        //TODO: snd + animation
    }

    public void DragArrow()
    {
        SetBowstringPositionRelativeToArrow();
        UpdateLineRenderer();
    }

    public void ReleaseArrow()
    {
        //TODO: smooth bowstring return to orginal position
        //TODO: string vibrations
        ResetArrowHolderSpotPosition();
        UpdateLineRenderer();
    }
}
