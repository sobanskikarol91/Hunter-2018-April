using UnityEngine;
using System.Collections;
using System;

public class Ball : Obstacle
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] DistanceJoint2D joint;

    bool isLineCut;

    protected override void Start()
    {
        base.Start();
        SetLineRendererStartSettings();
        StartCoroutine(CheckLineCollision());
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

    IEnumerator CheckLineCollision()
    {
        Func<Vector3> HookPosition = () => joint.connectedBody.transform.position;
        float distance = Vector2.Distance(HookPosition(), transform.position);

        while (!isLineCut)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, HookPosition() - transform.position);

            Debug.Log(distance);
            Debug.DrawLine(transform.position, HookPosition(), Color.blue, distance);

            if (hit.collider != null && hit.collider.tag == TagManager.arrow)
            {
                lineRenderer.enabled = false;
                isLineCut = true;
                joint.connectedBody = null;
            }

            UpdateLineRenderer();
            yield return null;
        }
    }

}
