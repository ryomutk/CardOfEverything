using UnityEngine;

namespace Effects
{
    public abstract class TextEffectData :ScriptableObject
    {
        new public abstract EffectName name{get;}
        public abstract IVisualEffect GetMotion(Transform target,string content);
    }
}