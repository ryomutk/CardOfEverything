using System.Collections.Generic;
using UnityEngine;
using Effects;
using Trigger;

namespace Actor

{
    public class StatusModifier
    {
        protected virtual Dictionary<CharacterStatus, IVisualEffect> effectDictionary{get;set;}

        public StatusModifier(Dictionary<CharacterStatus, IVisualEffect> effectDicts)
        {
            this.effectDictionary = effectDicts;
        }

        public virtual void Modify(CharacterStatus status)
        {
            
        }
    }
}