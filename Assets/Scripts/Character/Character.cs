using UnityEngine;
using Effects;
using Trigger;
using System.Collections.Generic;

namespace Actor
{
    public abstract class Character : MonoBehaviour
    {
        protected abstract IVisualEffect EnterMotion { get; set; }
        protected abstract IVisualEffect ExitMotion { get; set; }

        public abstract int[] actionWeightArray{get;set;}
        public event CharacterAction OnDeath;


        


        //キャラのステータス変更を行いたいときの受付窓口。
        /// <summary>
        /// actionWeightArrayはそのまま変更かなー
        /// </summary>
        public virtual void ModifyStatus(CharacterStatus target,float amount)
        {
            
        }

        public virtual void Enter()
        {
            BattleManager.instance.RegisterFX(EnterMotion);
        }

        public virtual void Exit()
        {
            BattleManager.instance.RegisterFX(ExitMotion);
        }
    }
}