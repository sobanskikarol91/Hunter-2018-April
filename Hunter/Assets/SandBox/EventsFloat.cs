using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[System.Serializable]
public class _UnityEventFloat : UnityEvent<float> { }

public class EventsFloat : MonoBehaviour
{
    public _UnityEventFloat changedLength;
    public _UnityEventFloat changedHeight;


    void ProcessValues(float v)
    {
        if (changedLength != null) changedLength.Invoke(1.4455f);
    }
}
