using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void ButtonAction();
public class CustomButton : MonoBehaviour
{
    public Transform home;
    public event ButtonAction onPointerDown;
    public event ButtonAction onHover;
    public event ButtonAction onOffHover;
    public event ButtonAction onPointerUp;
    public event ButtonAction onClick;

    KeyCode[] observeKeys = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse3 };

    ButtonAction nullAction;

    public bool clearEventsOnDisable = true;

    void Start()
    {
        nullAction = () => { };
    }

    public void AttachSelf(Transform target,Transform home)
    {
        this.home = home;
        transform.SetParent(target);
    }

    public void OnPointerDown()
    {
        ButtonActionQueue.RegisterAction(onPointerDown);
    }

    public void OnHover()
    {
        ButtonActionQueue.RegisterAction(onHover);
    }

    public void OnOffHover()
    {
        ButtonActionQueue.RegisterAction(onOffHover);
    }

    public void OnPointerUp()
    {
        ButtonActionQueue.RegisterAction(onPointerUp);
    }

    public void OnClick()
    {
        ButtonActionQueue.RegisterAction(onClick);
    }

    void OnDisable()
    {
        if (clearEventsOnDisable)
        {
            onPointerDown = nullAction;
            onPointerUp = nullAction;
            onClick = nullAction;
            onHover = nullAction;
            onOffHover = nullAction;
        }

        transform.SetParent(home);
    }

}
