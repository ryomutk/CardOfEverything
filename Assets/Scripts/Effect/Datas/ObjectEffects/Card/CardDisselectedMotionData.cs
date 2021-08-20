using UnityEngine;
using DG.Tweening;
using System;

namespace Effects
{


    public class CardDisselectedMotionData :GUIMotionData
    {

        public override GUIEffectName name { get { return GUIEffectName.card_motion_selected; } }
        [SerializeField] CardSelectMotion _cloneableInstance;
        protected override ObjectEffect cloneBase{get{return _cloneableInstance;}}

        [System.Serializable]
        class CardSelectMotion : ObjectEffect
        {
            Tween tween;
            [SerializeField] float duration = 0.5f;
            [SerializeField] string cursorTrigger = "Show";
            [SerializeField] float slideAmount = 0.7f;
            [SerializeField] Ease ease = Ease.InOutBounce;
            public override bool compleated{get;protected set;}

            public CardSelectMotion() : base(false)
            { }

            public override void SetTarget(GameObject target)
            {
                base.SetTarget(target);
                tween = target.transform.DOLocalMoveY(0, duration)
                .SetEase(ease);
                tween.onComplete = () => compleated = true;
            }

            public override void Execute(RendererGetter rendererGetter, AudioSource audioSource)
            {
                tween.Play();
            }
        }
    }
}