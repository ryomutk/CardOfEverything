using System;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(menuName = "GUIMotionData/TimeBarMotion")]
    public class TimeBarMotionData : CharacterStatusBarMotionData
    {
        public override GUIEffectName name { get { return GUIEffectName.timeBarMotion; } }
        [SerializeField] TimeBarMotion _timeBarMotion;
        protected override ObjectEffect cloneBase { get { return _timeBarMotion; } }

        [System.Serializable]
        protected class TimeBarMotion : CharacterStatusBarMotion
        {
            protected override void SetTargetNumNormalized()
            {
                var nowSession = BattleManager.instance.nowSession;

                if (nowSession is NowhereBattleSession nbs)
                {
                    targetNumNormalized = () => nbs.GetRemainTimeNormalized(targetCharacter);
                }
                else
                {
                    Debug.LogWarning("I am only for Nowhere battle session");
                }
            }
        }
    }
}