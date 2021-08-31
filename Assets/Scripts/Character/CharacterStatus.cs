using System.Collections.Generic;
using UnityEngine;
using Effects;

namespace Actor

{
    //キャラがダメージを受けた時のリアクションと値の管理。
    public class CharacterStatus
    {
        public Dictionary<CharacterStates,int> statusDictionary = new Dictionary<CharacterStates, int>();
        //しんだとき
        public event CharacterAction OnDeath;
        //被害を受けたとき
        public event System.Action<Character,CharacterStates,int> OnModify;
        protected Character master;


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