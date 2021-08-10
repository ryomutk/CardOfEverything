using UnityEngine;
using Actor;
using Utility;


namespace CardSystem
{
    public abstract class CardProfile
    {
        public abstract CardName id{get;protected set;}
        public abstract CharacterStatus statusRequirement{get;protected set;}
        public abstract AbilityAttribute attribute{get;protected set;}
        public abstract int defaultWeight{get;protected set;}

        public abstract Sprite thumbNail{get;protected set;}
        public abstract string flavorText{get;}
        
        public abstract string GetSimpleSummary(Character master);
        public abstract string GetDetail(Character master);

        
        public Card NewCard(Card instance)
        {
            instance.LoadProfile(this);
            return instance;
        }



        public abstract void Use(Character master);
    }
}