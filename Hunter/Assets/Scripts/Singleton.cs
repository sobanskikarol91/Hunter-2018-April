using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour
    where T : Component
{
    public static T instance { get; private set; }

    protected virtual void Awake()
    {
        if (instance != null)
            Debug.LogError(name + ": error: already initialized", this);

        instance = this as T;
    }
}
