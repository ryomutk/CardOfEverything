using UnityEngine;


namespace Effects
{
    public abstract class ObjectEffectData : ScriptableObject
    {
        new public abstract EffectName name { get; }
        public abstract IVisualEffect GetMotion(GameObject target);
    }
}
