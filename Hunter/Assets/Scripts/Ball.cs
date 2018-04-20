﻿using UnityEngine;
using System.Collections;
using System;

public class Ball : Obstacle
{
    protected override void CollisionWithArrow(Collision2D collision)
    {
        base.CollisionWithArrow(collision);
        ObjectPoolerManager.instance.SpawnFromPool("GoldParticle", collision.contacts[0].point, Quaternion.identity);
    }
}
