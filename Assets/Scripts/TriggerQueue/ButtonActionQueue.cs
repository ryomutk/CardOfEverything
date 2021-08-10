using System.Collections.Generic;

public static class ButtonActionQueue
{
    
    static List<ButtonAction> actionQueue;


    public static void RegisterAction(ButtonAction action)
    {
        actionQueue.Add(action);
    }

    public static bool Trigger()
    {
        int count = actionQueue.Count;
        for(int i = 0;i < count;i++)
        {
            actionQueue[0]();
            actionQueue.RemoveAt(0);
        }

        return count != 0;
    }
}