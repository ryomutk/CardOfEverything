using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Effects
{
    [CreateAssetMenu]
    public class DamageTextEffectData : TextEffectData
    {
        public override TextEffectName name { get { return TextEffectName.damage_text_effect; } }
        [SerializeField] DamageTextEffect _cloneBase = new DamageTextEffect();
        protected override TextEffect cloneBase { get { return _cloneBase; } }

        [System.Serializable]
        class DamageTextEffect : TextEffect
        {

            [SerializeField] TMP_FontAsset useFont;
            [SerializeField] float duration = 0.5f;
            [SerializeField] Ease ease = Ease.OutQuad;
            [SerializeField] float moveSize = 1;
            public override bool compleated { get; protected set; }

            public DamageTextEffect() : base(false)
            { }

            public override void SetContent(Transform target, string contents)
            {
                base.SetContent(target, contents);
            }


            public override void Execute(RendererGetter rendererGetter, AudioSource source)
            {
                var sq = DOTween.Sequence();
                var text = rendererGetter.GetTextObj();
                if (text.font != useFont)
                {
                    text.font = useFont;
                }

                sq.onComplete = () => compleated = true;

                text.transform.position = target.position;
                text.color = Color.clear;


                sq.Append(text.transform.DOLocalMoveY(moveSize, duration).SetRelative())
                .Join(text.DOFade(1, duration / 3))
                .Append(text.DOFade(0, duration * 2 / 3));

                sq.SetEase(ease);

                sq.onComplete += () =>
                {
                    compleated = true;
                    text.gameObject.SetActive(false);
                };


                sq.Play();
            }
        }
    }
}