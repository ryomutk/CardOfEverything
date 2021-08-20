using UnityEngine;
using DG.Tweening;
using System;

namespace Effects
{
    [CreateAssetMenu]
    public class CardSelectMotionData : GUIMotionData
    {
        public override GUIEffectName name { get { return GUIEffectName.card_motion_selected; } }
        [SerializeField] CardSelectMotion cloneableInstance;
        protected override ObjectEffect cloneBase{get{return cloneableInstance;}}

        [System.Serializable]
        class CardSelectMotion : ObjectEffect
        {
            Tween tween;
            [SerializeField] float duration = 0.5f;
            [SerializeField] string cursorTrigger = "Show";
            [SerializeField] float slideAmount = 0.7f;
            [SerializeField] Ease ease = Ease.InOutBounce;
            [SerializeField] AudioClip selectedSound;
            public override bool compleated {get;protected set;}

            public CardSelectMotion() : base(false)
            { }

            public override void SetTarget(GameObject target)
            {

                tween = target.transform.DOLocalMoveY(target.transform.position.y + slideAmount,duration)
                .SetEase(ease);
                tween.onComplete = () => compleated = true;
            }

            public override void Execute(RendererGetter rendererGetter, AudioSource audioSource)
            {
                audioSource.PlayOneShot(selectedSound);
                tween.Play();
            }
        }
    }
}