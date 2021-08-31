using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public delegate void ButtonAction(InputArg arg);
public class CustomButton : UIBehaviour
{
    public event ButtonAction buttonEvent;

    KeyCode[] observeKeys = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse3 };
    ButtonAction nullAction;


    public bool clearEventsOnDisable = true;

    protected override void Start()
    {
        base.Start();
        nullAction = (x) => { };
    }

    public void AttachSelf(GameObject target)
    {
        target.AddComponent<HomingInstinct>().ChangeParent(transform, target);
    }


    public void OnPointerDown()
    {
        var arg = new InputArg();
        for (int i = 0; i < observeKeys.Length; i++)
        {
            if (Input.GetKeyDown(observeKeys[i]))
            {
                arg.key = observeKeys[i];
                break;
            }
        }

        arg.type = InputType.pointerDown;

        ButtonActionQueue.instance.RegisterAction(() => buttonEvent(arg));
    }

    public void OnHover()
    {
        var arg = new InputArg();

        arg.type = InputType.hoverStart;
        arg.key = KeyCode.None;
        ButtonActionQueue.instance.RegisterAction(() => buttonEvent(arg));
    }

    public void OnOffHover()
    {
        var arg = new InputArg();

        arg.type = InputType.hoverEnd;
        arg.key = KeyCode.None;
        ButtonActionQueue.instance.RegisterAction(() => buttonEvent(arg));
    }

    public void OnPointerUp()
    {
        var arg = new InputArg();

        arg.type = InputType.pointerUp;

        for (int i = 0; i < observeKeys.Length; i++)
        {
            if (Input.GetKeyUp(observeKeys[i]))
            {
                arg.key = observeKeys[i];
                break;
            }
        }

        ButtonActionQueue.instance.RegisterAction(() => buttonEvent(arg));
    }

    public void OnClick()
    {
        var arg = new InputArg();

        arg.type = InputType.Click;

        for (int i = 0; i < observeKeys.Length; i++)
        {
            if (Input.GetKey(observeKeys[i]))
            {
                arg.key = observeKeys[i];
                break;
            }
        }

        Debug.Log("click");
        ButtonActionQueue.instance.RegisterAction(() => buttonEvent(arg));
    }

    //わが子がいなくなったら
    void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0)
        {
            //死ぬ
            gameObject.SetActive(false);
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (clearEventsOnDisable)
        {
            buttonEvent = nullAction;
        }

        //transform.SetParent(home);
    }

}
