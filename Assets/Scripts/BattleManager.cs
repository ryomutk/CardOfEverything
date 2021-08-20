using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Utility;
using Trigger;
using Actor;
using Effects;

public delegate void CharacterAction(Character target);


[RequireComponent(typeof(AudioSource), typeof(RendererGetter))]
public class BattleManager : Singleton<BattleManager>
{
    List<IInteraptor> interaptorQueue;
    VisualEffectQueue battleQueue;
    public BattleState battleState{get;private set;}

    //一応公開しない。なぜならできないときとかあるかもだし。
    public BattleSession nowSession{get;private set;}


    /// <summary>
    /// TurnStateが投げられるEvent
    /// </summary>
    public event System.Action<TurnState> OnTurnEvent;

    /// <summary>
    /// BattleStateが投げられるEvent
    /// </summary>
    public event System.Action<BattleState> OnBattleEvent;


    void Start()
    {
        var audio = GetComponent<AudioSource>();
        var getter = GetComponent<RendererGetter>();
        battleQueue = new VisualEffectQueue(getter, audio, StartCoroutine);
    }



    IEnumerator TurnLoop(BattleSession session)
    {
        battleState = BattleState.inBattle;
        TurnState[] states = System.Enum.GetValues(typeof(TurnState)) as TurnState[]; 

        while(!nowSession.IfBattleEnd)
        {
            for(int i = 0;i < states.Length;i++)
            {
                OnTurnEvent(states[i]);
                //入力、GUI待ち
                yield return null;
                battleQueue.Trigger();
                yield return StartCoroutine(HandleInteraptors());

                var additionalCount = battleQueue.Trigger();

                while(additionalCount > 0)
                {
                    yield return StartCoroutine(HandleInteraptors());
                    additionalCount = battleQueue.Trigger();
                }
            }
        }
    }

    public void RegisterFX(IVisualEffect effect)
    {
        battleQueue.RegisterFX(effect);
    }



    /// <param name="interaptor"></param>
    public void RegisterInterapt(IInteraptor interaptor)
    {
        interaptorQueue.Add(interaptor);
    }



    IEnumerator HandleInteraptors()
    {
        while (interaptorQueue.Count != 0)
        {
            yield return new WaitUntil(() => interaptorQueue[0].finished);
            interaptorQueue.RemoveAt(0);
        }
    }
}