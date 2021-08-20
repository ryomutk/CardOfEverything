using UnityEngine;
using System.Collections.Generic;
using Utility.ObjPool;
using Utility;
using System.Collections;
using CardSystem;

public class CardServer : Singleton<CardServer>, IInteraptor
{
    List<CardActionBase> actionList;
    CardViewProfile[] profiles;
    [SerializeField] Card rawCardPref;
    [SerializeField] int initNum = 10;
    InstantPool<Card> rawCardPool;
    public bool finished { get; private set; }

    void Start()
    {
        GameManager.instance.onGameEvent += (x) =>
        {
            if (x == GameState.serverInitialize)
            {
                StartCoroutine(Initialize());
            }
        };
    }

    IEnumerator Initialize()
    {
        finished = false;
        GameManager.instance.RegisterInterapt(this);

        profiles = CardProfileBuilder.GetAll();

        yield return null;

        var task = rawCardPool.CreatePoolAsync(rawCardPref, initNum);
        yield return new WaitUntil(() => task.IsCompleted);


        var fileNames = System.IO.Directory.GetFiles(Application.dataPath + "/Resources/Scriptables/CardActions/");
        for (int i = 0; i < fileNames.Length; i++)
        {
            var request = Resources.LoadAsync("Scriptables/CardActions/" + fileNames[i]);
            yield return new WaitUntil(() => request.isDone);
            actionList.Add(request.asset as CardActionBase);
        }

        finished = true;
    }

    public Card GetCard(CardName id)
    {
        var profile = GetProfile(id);
        var action = GetActionBase(id);
        if (profile != null)
        {
            var instance = rawCardPool.GetObj();
            instance.Initialize(profile, action);

            return instance;
        }
        //こいつは…バグカード的な?

        return Instantiate(rawCardPref);
    }


    public CardViewProfile GetProfile(CardName id)
    {
        for (int i = 0; i < profiles.Length; i++)
        {
            if (profiles[i].name == id)
            {
                return profiles[i];
            }
        }

        return null;
    }

    CardActionBase GetActionBase(CardName name)
    {
        for (int i = 0; i < actionList.Count; i++)
        {
            if (actionList[i].name == name)
            {
                return actionList[i];
            }
        }

        return null;
    }

}
