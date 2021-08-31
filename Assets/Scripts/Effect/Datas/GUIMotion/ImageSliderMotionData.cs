using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;
using Sirenix.OdinInspector;

namespace Effects
{
    //数値(Normalized)でImageの長さを変えるマン。
    //観測するための数字が必要なので継承して使おう。
    public abstract class ImageSliderMotionData : GUIMotionData
    {

        [SerializeField]
        protected abstract class ImageSliderMotion : ObjectEffect
        {
            protected Image targetRenderer;
            [SerializeField] Sprite baseSprite;
            [SerializeField] protected bool homingToTarget = true;

            //こいつが基準。
            protected abstract System.Func<float> targetNumNormalized { get; set; }


            float lastScale;
            //出現時にうにょーんってするか
            [SerializeField] bool hasEnterMotion = true;
            [SerializeField] float initialScale;
            //縦か？(変えるScaleがYか？と等価)
            [SerializeField] bool isVertical = false;
            [SerializeField] protected Color defaultColor;
            [SerializeField, ShowIf("hasEnterMotion")] float enterDuration = 2;
            [SerializeField] float changeDuration;
            [SerializeField] Ease changeEase = Ease.OutQuad;
            public override bool compleated { get; protected set; }

            protected ImageSliderMotion() : base(false)
            { }

            public override void SetTarget(MonoBehaviour target)
            {
                base.SetTarget(target);

                if (hasEnterMotion)
                {
                    dontDisturb = true;
                }
            }

            public override void Execute(RendererGetter rendererGetter, AudioSource audioSource)
            {
                compleated = false;

                if (dontDisturb == true)
                {
                    if (targetRenderer == null)
                    {
                        targetRenderer = rendererGetter.GetImageObj();
                        targetRenderer.sprite = baseSprite;
                        targetRenderer.color = defaultColor;
                    }


                    //これは直前にSetTargetされ、なおかつEnterMotionがあるときと等価。
                    if (isVertical)
                    {
                        targetRenderer.rectTransform.DOScaleY(initialScale, enterDuration);
                    }
                    else
                    {
                        targetRenderer.rectTransform.DOScaleX(initialScale, enterDuration);
                    }

                    lastScale = initialScale;
                    compleated = true;
                    dontDisturb = false;
                }

                var targetNum = targetNumNormalized();
                //変化していたら
                if (lastScale != targetNum)
                {
                    Tween tw;
                    //更新
                    if (isVertical)
                    {
                        tw = targetRenderer.rectTransform.DOScaleY(targetNum, changeDuration);
                    }
                    else
                    {
                        tw = targetRenderer.rectTransform.DOScaleX(targetNum, changeDuration);
                    }

                    tw.SetEase(changeEase);

                    tw.Play().onComplete += () => compleated = true;

                    lastScale = targetNum;
                }
            }
        }
    }
}