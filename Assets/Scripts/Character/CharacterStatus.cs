using System.Collections.Generic;
using UnityEngine;
using Effects;
using Trigger;

namespace Actor

{
    //キャラがダメージを受けた時のリアクションと値の管理。
    public class CharacterStatus
    {
        public Dictionary<CharacterStates,int> statusDictionary;
        //しんだとき
        public event CharacterAction OnDeath;
        //被害を受けたとき
        public event System.Action<Character,CharacterStates,int> OnModify;
        Character master;


        public virtual void Modify(Character character,CharacterStates status,int ammount)
        {            
            statusDictionary[status] += ammount;
            OnModify(character,status,ammount);
        }

        public void Initialize()
        {
            OnDeath = null;
            OnModify = null;
        }

    }
}