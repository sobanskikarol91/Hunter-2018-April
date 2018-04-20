using UnityEngine;

public class ForceField : MonoBehaviour 
{
    public string eventName;

    Animator animator;
    new Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        EventManager.StartListening(eventName, DisableForceField);
    }

    void DisableForceField()
    {
        Debug.Log("jaaa!");
        animator.SetTrigger("DisableForceField");
        collider.enabled = false;    
    }
}
