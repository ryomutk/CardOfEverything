using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace Effects
{
    //キャラクターに沿って動くステータスバーたち。
    public abstract class CharacterStatusBarMotionData : ImageSliderMotionData
    {
        [System.Serializable]
        protected abstract class CharacterStatusBarMotion : ImageSliderMotion
        {
            //表示場所のオフセット。0.5,0.5がキャラのど真ん中で0,0が左下。
            [SerializeField, ShowIf("homingToTarget")] Vector2 offset = new Vector2(0.5f, 0.5f);
            //実際にオフセットとして足されるポジション
            Vector2 offsetPosition;
            protected Actor.Character targetCharacter;

            protected override Func<float> targetNumNormalized { get; set; }

            public override void SetTarget(MonoBehaviour target)
            {
                base.SetTarget(target);

                if (target is Actor.Character character)
                {
                    targetCharacter = character;
                    SetTargetNumNormalized();
                }
                else
                {
                    Debug.Log("I only can handle character!");
                }

                var cSize = targetCharacter.viewRenderer.rectTransform.sizeDelta;

                //0,5 0,5の時が真ん中(修正なし)であるのでこの計算。
                offsetPosition = Vector2.Scale(offsetPosition, offset - Vector2.one / 2);
            }

            //ここで観測対象を設定。
            protected abstract void SetTargetNumNormalized();

            public override void Execute(RendererGetter rendererGetter, AudioSource audioSource)
            {
                base.Execute(rendererGetter, audioSource);

                if (homingToTarget && targetCharacter != null && targetRenderer != null)
                {
                    targetRenderer.rectTransform.position = targetCharacter.viewRenderer.rectTransform.position;
                    targetRenderer.rectTransform.position += (Vector3)offsetPosition;
                }
            }
        }
    }

}
