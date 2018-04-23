using UnityEngine;

public class FloatingText : MonoBehaviour, IObjectPooler
{
    Animator animator;
    [SerializeField] float SetInactiveTime = 2f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        Invoke("DisableAfterTime", SetInactiveTime);
    }

    void DisableAfterTime()
    {
        gameObject.SetActive(false);
    }

    public void PrepareObjectToSpawn()
    {
        animator.Play("ShowFloatingText", -1, 0f);

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.Play();
    }
}