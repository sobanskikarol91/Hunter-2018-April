using UnityEngine;

public class ForceField : Obstacle 
{
    public string eventName;

    new Collider2D collider;

    protected override void Start()
    {
        base.Start();
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        EventManager.StartListening(eventName, DisableForceField);
    }

    private void OnDisable()
    {
        EventManager.StopListening(eventName, DisableForceField);
    }

    void DisableForceField()
    {
        animator.SetTrigger("DisableForceField");
        collider.enabled = false;    
    }
}
