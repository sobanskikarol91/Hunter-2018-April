using UnityEngine;
using System.Collections;
using System;

public class Ball : Obstacle
{
    protected override void CollisionWithArrow(Collision2D collision)
    {
        base.CollisionWithArrow(collision);
        if (collision.contacts.Length > 0)
            ObjectPoolerManager.instance.SpawnFromPool("GoldParticle", collision.contacts[0].point, Quaternion.identity);
        else
            Debug.Log("No contacts points with ball");
    }
}
