using UnityEngine;
using System.Collections;

public class Trampoline : Obstacle
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void CollisionWithArrow(Collision2D collision)
    {
        base.CollisionWithArrow(collision);
        if (collision.contacts.Length > 0)
            ObjectPoolerManager.instance.SpawnFromPool("TrampolineParticle", collision.contacts[0].point, Quaternion.identity);
    }
}
