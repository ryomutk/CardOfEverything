using UnityEngine;
using Actor;
using Utility;


namespace CardSystem
{
    public abstract class CardActionBase
    {
        public abstract CardName name{get;}
        public abstract string GetDetail(Character master);
        public abstract void Execute(Character target);
    }
}