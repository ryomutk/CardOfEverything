using UnityEngine;

namespace Effects
{
    /// <summary>
    /// Gui表示だけのために使われるデータ
    /// </summary>
    public abstract class GUIMotionData:ScriptableObject
    {
        new public abstract GUIEffectName name{get;}
        protected abstract ObjectEffect cloneBase{get;}

        protected ObjectEffect InitMotion(GameObject target)
        {
            var instance = cloneBase.Clone();
            instance.SetTarget(target);

            return instance;
        }

        public virtual ObjectEffect GetMotion(GameObject target)
        {
            var instance = cloneBase.Clone();
            
            return instance;
        }
    }
}