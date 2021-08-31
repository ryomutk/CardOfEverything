using Utility;
using Effects;
using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(RendererGetter), typeof(AudioSource))]
public class GameManager : Singleton<GameManager>
{
    public event Action<GameState> onGameEvent;
    VisualEffectQueue GUIQueue;
    List<IInteraptor> interaptorQueue = new List<IInteraptor>();
    bool inputIsActive = false;
    bool guiIsActive = false;



    void Start()
    {
//        onGameEvent += (x) => Debug.Log("GamEv:"+x);


        var renderer = GetComponent<RendererGetter>();
        var audioSource = GetComponent<AudioSource>();
        GUIQueue = new VisualEffectQueue(renderer, audioSource, (x) => StartCoroutine(x));

        StartCoroutine(GameLoop());
    }

    //今はInitialize終わったらバトル始めるだけの野蛮人。
    System.Collections.IEnumerator GameLoop()
    {
        //OnInitialize受付期間
        yield return 2;

        onGameEvent(GameState.serverInitialize);
        yield return StartCoroutine(HandleInteraptors());
        onGameEvent(GameState.systemInitialize);
        yield return StartCoroutine(HandleInteraptors());
        onGameEvent(GameState.viewInitialize);
        yield return StartCoroutine(HandleInteraptors());

        guiIsActive = true;
        inputIsActive = true;

        onGameEvent(GameState.startGame);

        yield return StartCoroutine(HandleInteraptors());

        onGameEvent(GameState.inBattle);
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
        if (inputIsActive)
        {
            ButtonActionQueue.instance.Trigger();
        }

        if (guiIsActive)
        {
            GUIQueue.Trigger();
        }
    }
}

