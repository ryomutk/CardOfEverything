using System;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(menuName = "GUIMotionData/TimeBarMotion")]
    public class TimeBarMotionData : ImageSliderMotionData
    {
        public override GUIEffectName name { get { return GUIEffectName.timeBarMotion; } }
        [SerializeField] TimeBarMotion _timeBarMotion;
        protected override ObjectEffect cloneBase { get { return _timeBarMotion; } }

        [System.Serializable]
        protected class TimeBarMotion : ImageSliderMotion
        {
            protected override Func<float> targetNumNormalized { get; set; }

            public override void SetTarget(MonoBehaviour target)
            {
                base.SetTarget(target);

                if(target is Actor.Character character)
                {
                    var targetCharacter = character;
                    var nowSession = BattleManager.instance.nowSession;

                    if(nowSession is NowhereBattleSession nbs)
                    {
                        targetNumNormalized = () => nbs.GetRemainTimeNormalized(character);
                    }
                    else
                    {
                        Debug.Log("I am only for Nowhere battle session");
                    }
                }
                else
                {
                    Debug.Log("I only can handle character!");
                }
            }
        }
    }
}