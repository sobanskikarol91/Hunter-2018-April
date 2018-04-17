using UnityEngine;

public class Switch : Obstacle 
{
    bool isTurnOn;

    protected new void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTurnOn) return;

        isTurnOn = true;
        CollisionWithArrow(collision);
        ObjectPoolerManager.instance.SpawnFromPool("RedParticle", collision.contacts[0].point, Quaternion.identity);
        EventManager.TriggerEvent("Switch");
    }
}
