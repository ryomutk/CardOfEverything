using UnityEngine;


namespace CardSystem
{
    //カードの見た目。
    //テキストの配置や
    //ボタンを押したときの変化も含む見た目を決める
    [System.Serializable]
    public class CardFormatter
    {
        [SerializeField] Sprite frameSprite;
        [SerializeField] Sprite pushedFrame;

        public void Format(Card card)
        {
            
        }

        public static ButtonAction FormatCallback()
        {

        }
    }
}