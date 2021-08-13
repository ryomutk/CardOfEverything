using UnityEngine;
using System.Collections.Generic;
using Utility.ObjPool;
using Utility;

namespace CardSystem
{
    public class CardServer : Singleton<CardServer>
    {
        List<CardActionBase> actionList;
        CardViewProfile[] profiles;
        [SerializeField] Card rawCardPref;
        [SerializeField] int initNum = 10;
        InstantPool<Card> rawCardPool;

        protected override void Awake()
        {
            base.Awake();
            profiles = CardProfileBuilder.GetAll();   
            rawCardPool.CreatePool(rawCardPref,initNum);
        }

        public Card GetCard(CardName id)
        {
            var profile = GetProfile(id);
            var action = GetActionBase(id);
            if(profile != null)
            {
                var instance = rawCardPool.GetObj();
                instance.Initialize(profile,action);

                return instance;
            }
            //こいつは…バグカード的な?

            return Instantiate(rawCardPref);
        }


        CardViewProfile GetProfile(CardName id)
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
            for(int i = 0;i < actionList.Count;i++)
            {
                if(actionList[i].name == name)
                {
                    return actionList[i];
                }
            }
            
            return null;
        }

    }
}