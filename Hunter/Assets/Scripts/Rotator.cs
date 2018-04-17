using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    new Transform transform;
    [SerializeField] float speed = 1;

    private void Start()
    {
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), speed);
    }
}
