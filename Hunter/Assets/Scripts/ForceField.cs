using UnityEngine;

public class ForceField : Obstacle 
{
    public string disableEventName;
    public string hitEventName;

    new Collider2D collider;

    protected override void Start()
    {
        base.Start();
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        EventManager.StartListening(disableEventName, DisableForceField);
        EventManager.StartListening(hitEventName, HitForceField);
    }

    private void OnDisable()
    {
        EventManager.StopListening(disableEventName, DisableForceField);
        EventManager.StopListening(hitEventName, HitForceField);
    }

    void DisableForceField()
    {
        animator.SetTrigger("DisableForceField");
        collider.enabled = false;    
    }

    void HitForceField()
    {
        animator.SetTrigger("ArrowHit");
    }
    
    protected override void CollisionWithArrow(Collision2D collision)
    {
        EventManager.TriggerEvent(hitEventName);
        audioSource.Play();
        if (collision.contacts.Length > 0)
            ObjectPoolerManager.instance.SpawnFromPool("TrampolineParticle", collision.contacts[0].point, Quaternion.identity);
    }
}
