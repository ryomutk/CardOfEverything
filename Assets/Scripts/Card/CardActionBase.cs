using UnityEngine;
using Actor;
using Utility;


namespace CardSystem
{
    public abstract class CardActionBase:ScriptableObject
    {
        new public abstract CardName name{get;}
        public abstract string GetDetail(Character master);
        public abstract void Execute(Character master);
    }
}