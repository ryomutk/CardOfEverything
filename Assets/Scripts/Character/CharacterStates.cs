using System;

namespace Actor
{
    //キャラのステ一覧、数的な数の基準はクトゥルフの50倍くらいが良いかなと思う
    [Flags]
    public enum CharacterStates
    {
        none,
        hp = 1,        //プレイヤー上限3000程度。敵は比較的高く。
        strength = 2,  //物理的な力を洗わす。物理攻撃力に直結。
        dexterity = 4, //どれほど器用にものを扱えるかに影響。
        diffence = 8, 
        power = 16,
        mental = 32,
        magic = 64,
        casting = 128
    }
}