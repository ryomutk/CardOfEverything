using UnityEngine;


namespace Effects
{
    public abstract class ObjectEffectData : ScriptableObject
    {
        new public abstract ObjEffectName name { get; }
        protected abstract ObjectEffect cloneBase { get; }

        //インスタンスを読み込む者
        protected ObjectEffect InitMotion(GameObject target)
        {
            var instance = cloneBase.Clone();
            instance.SetTarget(target);
            return instance;
        }

        public virtual ObjectEffect GetMotion(GameObject target)
        {
            var instance = InitMotion(target);

            return instance;
        }
    }
}
