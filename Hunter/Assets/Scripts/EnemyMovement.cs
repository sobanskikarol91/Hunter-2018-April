using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float duration = 2f;
    [SerializeField] Transform destiny;


    private void Start()
    {
        StartCoroutine(SmoothMovement());
    }


    IEnumerator SmoothMovement()
    {
        Vector3 origin = transform.position;

        while (true)
        {
            yield return StartCoroutine(IEnumeratorMethods.Lerp(origin, destiny.position, duration, AssignPosition));
            yield return StartCoroutine(IEnumeratorMethods.Lerp(destiny.position, origin, duration, AssignPosition));
        }
    }
    void AssignPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
