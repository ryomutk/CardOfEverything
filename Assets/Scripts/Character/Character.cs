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
        

        public event CharacterAction OnDeath;

        public virtual void ModifyStatus()
        {

        }

        public virtual void Enter()
        {
            VisualEffectQueue.RegisterFX(EnterMotion);
        }

        public virtual void Exit()
        {
            VisualEffectQueue.RegisterFX(ExitMotion);
        }
    }
}