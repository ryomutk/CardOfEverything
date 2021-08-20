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
    Player nowPlayer = null;

    public static Character player { get { return instance.nowPlayer; } }


    void Start()
    {
        GameManager.instance.onGameEvent += (x) =>
        {
            if (x == GameState.systemInitialize)
            {
                StartCoroutine(Initialize());
            }
        };
    }


    IEnumerator Initialize()
    {
        var profile = CharacterServer.instance.GetProfile(CharacterName.player);
        yield return null;

    }

    public bool IsPlayer(Character character)
    {
        throw new System.NotImplementedException();
    }

    void SavePlayerStatus()
    {
        
    }


}