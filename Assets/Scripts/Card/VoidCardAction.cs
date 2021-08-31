using Actor;
using UnityEngine;

namespace CardSystem
{
    [CreateAssetMenu]
    public class VoidCardAction:CardActionBase
    {
        public override CardName name{get {return CardName.card_of_void;}}
        public override string GetDetail(Actor.Character master)
        {
            return "俺は虚空教団員だぞ！！";
        }

        public override void Execute(Character target)
        {
            
            target.ModifyStatus(CharacterStates.hp,-100);
        }
    }
}