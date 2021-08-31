using UnityEngine;
using Actor;
using System.Collections.Generic;
using CardSystem;

//Nowhere in the square用のバトルセッションプロトコォル
public class NowhereBattleSession : BattleSession
{
    int nowCharacterCount = 0;
    int defaultDuration;

    Dictionary<Character, int> remainingTimes = new Dictionary<Character, int>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="loopDuration">これが0以下になるとあれ。</param>
    /// <param name="profile"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public NowhereBattleSession(int loopDuration, SessionProfile profile, BattleField field) : base(profile, field)
    {
        defaultDuration = loopDuration;
        BattleManager.instance.OnTurnEvent += (x) =>
        {
            TurnSequencer(x);
        };
    }

    public override Character AddCharacter(CharacterName name)
    {
        var instance = base.AddCharacter(name);
        remainingTimes[instance] = defaultDuration;

        return instance;
    }

    public float GetRemainTimeNormalized(Character character)
    {
        var num =remainingTimes[character] / defaultDuration;
        return num;
    }

    public void ModifyCoolTime(Character character, int amount)
    {
        remainingTimes[character] += amount;
    }


    void TurnSequencer(TurnState state)
    {

        if (nowCharacterCount == characterInBattle.Count)
        {
            nowCharacterCount = 0;
        }

        var chara = characterInBattle[nowCharacterCount];

        if (state == TurnState.turnStart)
        {
            //上書き。
            chara = characterInBattle[nowCharacterCount];

            TurnStartProcess(chara);

        }
        else if (state == TurnState.cardDraw)
        {
            if (remainingTimes[chara] <= 0)
            {
                CardDrawProcess(chara);
            }
        }
        else if (state == TurnState.turnEnd)
        {
            nowCharacterCount++;
        }
    }

    void TurnStartProcess(Character chara)
    {
        if (remainingTimes[chara] <= 0)
        {
            remainingTimes[chara] = defaultDuration;
        }

        var remainTime = remainingTimes[chara];

        remainTime -= chara.Status.statusDictionary[CharacterStates.dexterity];

    }

    /// <summary>
    /// キャラのクールダウンタイムが0になったときのプロセス
    /// </summary>
    /// <param name="chara"></param>
    void CardDrawProcess(Character chara)
    {
        var drawnCard = BattleManager.instance.deckHolder.GetWeightData(chara).DrawCard();

        //プレイヤーの場合は
        if (PlayerManager.instance.IsPlayer(chara))
        {
            var instance = CardServer.instance.GetCard(drawnCard);

            //Handに送る。
            CardRouter.instance.SetCard(Hand.instance, drawnCard);
        }
        else
        {
            var action = CardServer.instance.GetActionBase(drawnCard);

            //仮
            action.Execute(chara);
        }
    }

    protected override void OnKilled(Character target)
    {
        remainingTimes.Remove(target);
        base.OnKilled(target);
    }

    void PlayerSequence()
    {
        throw new System.NotImplementedException();
    }
}