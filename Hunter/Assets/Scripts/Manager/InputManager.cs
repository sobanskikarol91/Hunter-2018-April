using UnityEngine;
using System.Collections;
using System;

public class MouseEvent : EventArgs
{
    Vector3 mousePosition;

    public MouseEvent(Vector3 mousePosition)
    {
        this.mousePosition = mousePosition;
    }
}

public class InputManager : Singleton<InputManager>
{
    public event EventHandler OnButtonDown = delegate { };
    public event EventHandler OnButtonHold = delegate { };
    public event EventHandler OnButtonUp = delegate { };

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnButtonDown(this, new MouseEvent(Input.mousePosition));
    }
}
