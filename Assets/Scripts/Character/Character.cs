using UnityEngine;
using Effects;
using Trigger;
using System.Collections.Generic;

namespace Actor
{
    public abstract class Character : MonoBehaviour
    {
        public abstract int[] actionWeightArray{get;set;}
        
        CharacterStatus _status = new CharacterStatus();
        public CharacterStatus Status{get{return _status;}}
        public CardSystem.CardName[] cardList{get;private set;}
        
        public event CharacterAction OnEnter;
        public event CharacterAction OnExit;

        //
        public abstract CardSystem.Card DrawCard();



        //キャラのステータス変更を行いたいときの受付窓口。
        /// <summary>
        /// actionWeightArrayはそのまま変更かなー
        /// </summary>
        public virtual void ModifyStatus(CharacterStates target,int amount)
        {
            Status.Modify(this,target,amount);
        }

        public virtual void Enter()
        {
            OnEnter(this);
        }

        public virtual void Exit()
        {
            OnExit(this);
        }

        void OnDisable()
        {
            OnEnter = null;
            OnExit = null;
        }
    }
}