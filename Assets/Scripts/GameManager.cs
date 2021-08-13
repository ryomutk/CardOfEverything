using Utility;
using Trigger;
using Effects;
using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(RendererGetter), typeof(AudioSource))]
public class GameManager : Singleton<GameManager>
{
    public event Action onInitialize;
    VisualEffectQueue GUIQueue;
    GameState nowState;
    List<IInteraptor> interaptorQueue = new List<IInteraptor>();


    void Start()
    {
        var renderer = GetComponent<RendererGetter>();
        var audioSource = GetComponent<AudioSource>();
        GUIQueue = new VisualEffectQueue(renderer, audioSource, (x) => StartCoroutine(x));
    }

    System.Collections.IEnumerator GameLoop()
    {
        //OnInitialize受付期間
        yield return 2;

        onInitialize();
        yield return StartCoroutine(HandleInteraptors());

        
    }

    public void RegisterInterapt(IInteraptor interaptor)
    {
        interaptorQueue.Add(interaptor);
    }



    System.Collections.IEnumerator HandleInteraptors()
    {
        while (interaptorQueue.Count != 0)
        {
            yield return new WaitUntil(() => interaptorQueue[0].finished);
            interaptorQueue.RemoveAt(0);
        }
    }

    public void RegisterGUIMotion(IVisualEffect effect)
    {
        GUIQueue.RegisterFX(effect);
    }

    void Update()
    {
        GUIQueue.Trigger();
    }
}

