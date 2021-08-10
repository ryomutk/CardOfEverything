using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Utility;
using Trigger;
using Actor;

public delegate void CharacterAction(Character target);

public class BattleManager:Singleton<BattleManager>
{
    List<IInteraptor> interaptorQueue;
    VisualEffectQueue viewQueue;
    //InputQueueはStaticクラス。

    

    void Start()
    {
        viewQueue = VisualEffectQueue.instance;
    }


    /// <summary>
    /// Trigger君へ。
    /// interaptはひとつづつじゃなくて初めにいっぺんに登録してね
    /// あなたのManagerより
    /// </summary>
    /// <param name="interaptor"></param>
    public void RegisterInterapt(IInteraptor interaptor)
    {
        interaptorQueue.Add(interaptor);
    }

    

    IEnumerator HandleInteraptors()
    {
        while(interaptorQueue.Count != 0)
        {
            yield return new WaitUntil(()=> interaptorQueue[0].finished);
            interaptorQueue.RemoveAt(0);
        }
    }
}