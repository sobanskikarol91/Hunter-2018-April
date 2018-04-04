using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    public Transform player;

    private void Update()
    {
        Vector2 direction = player.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed);
    }
}
