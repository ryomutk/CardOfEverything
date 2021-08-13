using UnityEngine;
using System;

namespace Effects
{
    public abstract class ObjectEffect : IVisualEffect
    {
        public GameObject target{get;private set;}
        public EffectName name {get;private set;}
        public bool dontDisturb{get;set;}
        public event Action onCompleate;
        public bool compleated{get;}

        public ObjectEffect(EffectName name,GameObject target,bool dontDisturb = false)
        {
            compleated =false;
            this.name = name;
            this.target = target;
            this.dontDisturb = dontDisturb;
        }

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public abstract void Execute(RendererGetter rendererGetter,AudioSource audioSource);
    }
}