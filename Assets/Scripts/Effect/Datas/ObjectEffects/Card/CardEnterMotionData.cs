using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace Effects
{
    public class CardEnterMotionData : ObjectEffectData
    {
        public override ObjEffectName name { get { return ObjEffectName.card_motion_enter; } }
        [SerializeField] CardEnterMotion _cloneBase;
        protected override ObjectEffect cloneBase { get { return _cloneBase; } }



        class CardEnterMotion : ObjectEffect
        {
            [SerializeField] Vector3 enterPosition;
            [SerializeField] float duration = 0.5f;
            [SerializeField] float rotateCount = 3;
            [SerializeField] Ease ease = Ease.OutQuad;
            [SerializeField] int pathPoints = 8;
            Image targetGraphic;
            Vector3[] path;
            Tween tween;
            public override bool compleated { get; protected set; }
            public CardEnterMotion() : base(true)
            {}

            public override void SetTarget(GameObject target)
            {
                base.SetTarget(target);
                targetGraphic = target.GetComponentInChildren<Image>();
                if (targetGraphic != null)
                {
                    targetGraphic.color = Color.clear;

                }

                //多分丸められてupto360までの回転しかしてくんないから、その時は回転をTw側でつけてね。
                target.transform.localEulerAngles = new Vector3(0, 0, -360 * rotateCount);

                BuildTween();
            }

            void BuildTween()
            {
                var sq = DOTween.Sequence();

                var defaultPosition = target.transform.position;
                MakePath(defaultPosition);

                sq.Append(target.transform.DOPath(path, duration, PathType.CatmullRom));
                if (targetGraphic != null) { sq.Join(targetGraphic.DOFade(1, duration)); }
                sq.Join(target.transform.DOLocalRotate(Vector3.zero, duration));
                sq.SetEase(ease);
                sq.onComplete += () =>

                tween = sq;
            }

            void MakePath(Vector3 target)
            {
                for (int i = 0; i < pathPoints; i++)
                {
                    path[i] = Vector3.Slerp(enterPosition, target, i / pathPoints);
                }
            }

            public override void Execute(RendererGetter rendererGetter, AudioSource audioSource)
            {
                tween.Play();
            }
        }
    }
}