using UnityEngine;
using Utility;
using Actor;
using System.Collections;
using System.Collections.Generic;

//プレイヤーの初期化や情報の読み込み、ストレージを行い、
//プレイヤーの情報へのアクセスポイントや各種設定。を行う
public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] Character rawPlayerPref;
    List<Character> playerList = new List<Character>();
    Dictionary<Character,int> handNumberList = new Dictionary<Character, int>();
    public int playerNum{get{return playerList.Count;}}

    void Start()
    {
        GameManager.instance.onGameEvent += (x) =>
        {
            if (x == GameState.managerInitialize)
            {
                StartCoroutine(Initialize());
            }
        };
    }

    /// <summary>
    /// 今はプレイヤーは一人のみの想定。
    /// でも簡単に複数に増やせます。
    /// </summary>
    /// <returns></returns>
    IEnumerator Initialize()
    {
        var profile = CharacterServer.instance.GetProfile(CharacterName.player);
        var player = Instantiate(rawPlayerPref);
        profile.LoadToCharacter(player);

        playerList.Add(player);
        
        yield return null;

    }

    public bool IsPlayer(Character character)
    {
        return playerList.Contains(character);
    }

    //原初のプレイやーデータへのアクセス。
    public Character GetPlayer(int index)
    {
        return playerList[index];
    }

    void SavePlayerStatus()
    {
        
    }


}