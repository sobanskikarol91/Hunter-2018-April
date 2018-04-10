using UnityEngine;
using UnityEngine.UI;

public class StarController : MonoBehaviour 
{
    Animator animator;
    Image img;
    AudioSource audioSource;

    [SerializeField] Color32 destinyColor;
    [SerializeField] Color32 startColor;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        img = GetComponent<Image>();
    }

    public void LightStar()
    {
        animator.Play("LightStar", -1, 0f);
        img.color = destinyColor;
        audioSource.Play();
    }

    void OnEnable()
    {
        img.color = startColor;
    }
}
