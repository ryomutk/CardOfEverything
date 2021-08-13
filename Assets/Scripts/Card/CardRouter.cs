using UnityEngine;
using Utility;
using System;

namespace CardSystem
{
    public class CardRouter:Singleton<CardRouter>
    {
        public event System.Action<Card,InputArg> OnCardInput;
        
        public void SetCard(ICardViewPoint viewPoint,CardName name)
        {
            var cardInstance = CardServer.instance.GetCard(name);
            Action<InputArg> inputAction = (x) => OnCardInput(cardInstance,x);
            inputAction += (x) => viewPoint.OnInput(subject,BattleManager.instance.)
        }


    }
}