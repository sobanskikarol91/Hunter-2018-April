using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public Clip highlighted;
    public Clip clicked;
    AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetClip(highlighted);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ExtensionAudio.PlayAudioClip(clicked, transform.position);
    }

    void SetClip(Clip clip)
    {
        _audioSource.clip = clip.clip;
        _audioSource.outputAudioMixerGroup = clip.group;
        _audioSource.Play();
    }
}
