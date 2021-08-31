using System;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(menuName = "GUIMotionData/HpBarMotionData")]
    public class HpBarMotionData : ImageSliderMotionData
    {
        public override GUIEffectName name { get { return GUIEffectName.hpbarMotion; } }
        [SerializeField] HpBarMotion _hpBarMotion;
        protected override ObjectEffect cloneBase { get { return _hpBarMotion; } }

        [System.Serializable]
        protected class HpBarMotion : ImageSliderMotion
        {
            [SerializeField] Color lowHpColor = Color.red;
            Color colorDifference;


            Actor.Character targetCharacter;
            protected override Func<float> targetNumNormalized { get; set; }
            public override void SetTarget(MonoBehaviour target)
            {
                base.SetTarget(target);

                colorDifference = lowHpColor - defaultColor;

                if (target is Actor.Character character)
                {
                    targetCharacter = character;
                    var defaultHP = CharacterServer.instance.GetCharacterDefault(character.name, Actor.CharacterStates.hp);
                    targetNumNormalized = () => character.Status.statusDictionary[Actor.CharacterStates.hp] / defaultHP;
                }
                else
                {
                    Debug.Log("I only can handle character!");
                }
            }

            public override void Execute(RendererGetter rendererGetter, AudioSource audioSource)
            {
                base.Execute(rendererGetter, audioSource);
                targetRenderer.color = defaultColor + colorDifference*targetNumNormalized();
                
            }
        }


    }
}