using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class BigScript : MonoBehaviour
{
    [Header("Here's a cool event! Drag anything here!")]
    public UnityEvent whoa;

    void Start()
    {

    }
    private void YourFunction()
    {
        whoa.Invoke();
    }

}



