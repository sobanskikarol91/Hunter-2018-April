using UnityEngine;
using System.Collections;

public class Ball : Obstacle
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] DistanceJoint2D joint;

    protected override void Start()
    {
        base.Start();
        SetLineRendererStartSettings();
    }

    private void Update()
    {
        UpdateLineRenderer();
    }

    void SetLineRendererStartSettings()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, joint.connectedBody.position);
    }

    void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(1, (Vector2)transform.position + joint.anchor);
    }

    protected override void CollisionWithArrow(Collision2D collision)
    {
        base.CollisionWithArrow(collision);
        ObjectPoolerManager.instance.SpawnFromPool("GoldParticle", collision.contacts[0].point, Quaternion.identity);
    }
}
