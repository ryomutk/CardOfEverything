using UnityEngine;
using System.Collections.Generic;
using Actor;

//現在のバトルの状況を保持し、
//各種コンポーネントへのアクセスポイントになるActorのCardRouterみたいな？
public class BattleSession
{
    List<Character> characterInBattle;
    List<Character> killedList;
    BattleField field;
    public event System.Action<Character> OnCharacterKilled;
    public event System.Action<Character> OnCharacterSelected;
    public bool IfBattleEnd { get; private set; }

    public BattleSession(SessionProfile profile,BattleField field)
    {
        this.field = field;
        field.OnCharacterSelected += (x) => OnCharacterSelected(x);

        characterInBattle.Add(PlayerManager.player);

        foreach(var enemy in profile.GetCharactersInBattle())
        {
            AddCharacter(enemy);
        }
    }

    public Character AddCharacter(CharacterName name)
    {
        var instance = CharacterServer.instance.GetCharacter(name);
        field.AddCharacter(instance);
        instance.Enter();

        instance.Status.OnDeath += (x) => OnKilled(x);

        return instance;
    }

    void OnKilled(Character target)
    {
        killedList.Add(target);
        characterInBattle.Remove(target);
        CheckIfEnd();
    }

    void CheckIfEnd()
    {
        for(int i = 0; i < characterInBattle.Count;i++)
        {
            var target = characterInBattle[i];
            var result = PlayerManager.instance.IsPlayer(target);

            if(!result)
            {
                IfBattleEnd = false;
                return;
            }
        }

        IfBattleEnd = true;
    }

    public Character GetSelected()
    {
        return field.GetSelected();
    }
}