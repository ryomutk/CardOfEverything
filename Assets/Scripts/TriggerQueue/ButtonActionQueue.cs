using System.Collections.Generic;
using System;
using Utility;

public class ButtonActionQueue:Singleton<ButtonActionQueue>
{

    List<Action> actionQueue = new List<Action>();


    public void RegisterAction(Action action)
    {
        actionQueue.Add(action);
    }

    public bool Trigger()
    {
        if (actionQueue == null)
        {
            actionQueue = new List<Action>();
        }
        
        int count = actionQueue.Count;

        try
        {
            for (int i = 0; i < count; i++)
            {
                actionQueue[0]();
                actionQueue.RemoveAt(0);
            }
        }
        catch (System.NullReferenceException)
        {
            actionQueue.Clear();
        }

        return count != 0;
    }
}