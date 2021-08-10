using Utility;
using UnityEngine;
using System;
using Actor;

namespace CardSystem
{
    //CardProfileのJsonで保存できる範囲
    [Serializable]
    public class CardJsonProfile
    {
        [SerializeField] CardName _name = CardName.card_of_void;
        [SerializeField] CharacterStatus _statusRequirement = CharacterStatus.none;
        [SerializeField] AbilityAttribute _attribute = AbilityAttribute.Almighty;
        [SerializeField] int _defaultWeight = 300;
        [SerializeField] Sprite _thumbNail;
        [SerializeField] string _flavorText = "終焉の時が来た。\n 恐れることはない、貴方もまた、ここから生じたのだから。"; 

        public CardName name{get{return _name;} set{_name = value;}}
        public CharacterStatus statusRequirement{get{return _statusRequirement;} set{_statusRequirement = value;}}
        public AbilityAttribute attribute{get{return _attribute;} set{_attribute = value;}}
        public int defaultWeight {get {return _defaultWeight;} set{_defaultWeight = value;}}
        public Sprite thumbnail {get{return _thumbNail;} set{_thumbNail = value;}}
        public string flavorText {get{return _flavorText;} set{_flavorText = value;}}
    }
}