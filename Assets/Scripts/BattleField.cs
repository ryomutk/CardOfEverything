using UnityEngine;
using System.Collections.Generic;
using Actor;

/// <summary>
/// 敵を表示し、敵への入力の管理をするクラス。
/// </summary>
/// \
public class BattleField:MonoBehaviour
{
    List<Character> charasInBattle = new List<Character>();
    Character selected;
    public event System.Action<Character> OnCharacterSelected;
    LayoutField<Character> placer;

    void Start()
    {
        placer = GetComponent<LayoutField<Character>>();
    }


    public virtual void OnInput(Character subject)
    {
        selected = subject;
        placer.Place(subject);
        OnCharacterSelected(subject);
    }

    public void AddCharacter(Character character)
    {
        charasInBattle.Add(character);
        placer.Place(character);
    }

    public bool RemoveCharacter(Character character)
    {
        placer.Remove(character);
        return charasInBattle.Remove(character);
    }

    public Character GetSelected()
    {
        if(charasInBattle.Contains(selected))
        {
            return selected;
        }
        else
        {
            return charasInBattle[0];
        }
    }


}