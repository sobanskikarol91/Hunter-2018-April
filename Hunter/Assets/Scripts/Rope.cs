using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(LineRenderer), typeof(DistanceJoint2D), typeof(Rigidbody2D))]
public class Rope : MonoBehaviour
{
    LineRenderer lineRenderer;
    DistanceJoint2D joint;

    bool isLineCut;

    void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        lineRenderer = GetComponent<LineRenderer>();
        joint = GetComponent<DistanceJoint2D>();

        SetLineRendererStartSettings();
        StartCoroutine(CheckLineCollision());
    }

    void SetLineRendererStartSettings()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, joint.connectedBody.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    void UpdateLineRenderer()
    {
        lineRenderer.SetPosition(0, joint.connectedBody.position + joint.anchor);
    }

    IEnumerator CheckLineCollision()
    {
        Func<Vector3> HookPosition = () => joint.connectedBody.transform.position;
        float distance = Vector2.Distance(HookPosition(), transform.position);

        while (!isLineCut)
        {
            UpdateLineRenderer();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, HookPosition() - transform.position, distance + distance/10);

            if (hit.collider.isTrigger && hit.collider.tag == TagManager.arrow)
            {
                lineRenderer.enabled = false;
                isLineCut = true;
                joint.connectedBody = null;
            }


            yield return null;
        }
    }
}
