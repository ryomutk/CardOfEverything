using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Utility;
using Trigger;
using Actor;
using Effects;

public delegate void CharacterAction(Character target);


[RequireComponent(typeof(AudioSource),typeof(RendererGetter))]
public class BattleManager:Singleton<BattleManager>
{
    List<IInteraptor> interaptorQueue;
    VisualEffectQueue battleQueue;

    /// <summary>
    /// BattleStateが投げられるEvent
    /// </summary>
    public event System.Action<BattleState> OnBattleEvent;

    
    

    void Start()
    {
        var audio = GetComponent<AudioSource>();
        var getter = GetComponent<RendererGetter>();
        battleQueue = new VisualEffectQueue(getter,audio,StartCoroutine);
    }

    void Update()
    {
        ButtonActionQueue.instance.Trigger();
    }

    public void RegisterFX(IVisualEffect effect)
    {
        battleQueue.RegisterFX(effect);
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