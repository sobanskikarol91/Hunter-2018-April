using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Unity C# Event manager using UnityEvents and a Hashtable for loosely typed params with event.
This EventManager expands the usefulness of UnityEvents by allowing values of any type to be passed as a
parameter in the Event eg: int, string, Vector3 etc.

Usage:

// Add Listener for Event
SDKBoyEventManager.StartListening ("MY_EVENT", MyEventHandlerMethodName);

// Trigger Event:
SDKBoyEventManager.TriggerEvent ("MY_EVENT", new Hashtable(){{"MY_EVENT_KEY", "valueOfAnyType"}});

// Pass null instead of a Hashtable if no params
SDKBoyEventManager.TriggerEvent ("MY_EVENT", null);

// Handler
private void HandleTeleportEvent (Hashtable eventParams){
	if (eventParams.ContainsKey("MY_EVENT")){
		// DO SOMETHING
	}
}

*/

public class SDKBoyEvent : UnityEvent<Hashtable> { }

public class SDKBoyEventManager : MonoBehaviour
{

    private Dictionary<string, SDKBoyEvent> eventDictionary;

    private static SDKBoyEventManager eventManager;

    //	SINGLETON
    public static SDKBoyEventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(SDKBoyEventManager)) as SDKBoyEventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, SDKBoyEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction<Hashtable> listener)
    {
        SDKBoyEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new SDKBoyEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<Hashtable> listener)
    {
        if (eventManager == null) return;
        SDKBoyEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, Hashtable eventParams = default(Hashtable))
    {
        SDKBoyEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParams);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        TriggerEvent(eventName, null);
    }
}