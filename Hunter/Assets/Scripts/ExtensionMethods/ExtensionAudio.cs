using UnityEngine;


public class ExtensionAudio : MonoBehaviour
{
    public static AudioSource PlayAudioClip(Clip clip, Vector3 position)
    {
        GameObject go = new GameObject("One shot audio");
        go.transform.position = position;
        AudioSource source = go.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = clip.group;
        source.clip = clip.clip;
        source.Play();
        Destroy(go, clip.clip.length);
        return source;
    }
}
