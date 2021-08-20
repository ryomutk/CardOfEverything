using UnityEngine;
using System.Collections;
using Actor;
using Utility;

public class CharacterServer : Singleton<CharacterServer>, IInteraptor
{
    CharacterProfile[] characterProfiles;
    [SerializeField] Character rawCharacterPref;
    Utility.ObjPool.InstantPool<Character> characterPool;
    [SerializeField] int initNum = 10;
    public bool finished { get; private set; }

    void Start()
    {
        GameManager.instance.onGameEvent += (x) =>
        {
            if(x == GameState.serverInitialize)
            {
                StartCoroutine(Initialize());
            };
        };
    }

    IEnumerator Initialize()
    {
        finished = false;
        GameManager.instance.RegisterInterapt(this);
        characterProfiles = CharacterProfileBuilder.GetAll();

        var task = characterPool.CreatePoolAsync(rawCharacterPref, initNum);
        yield return new WaitUntil(() => task.IsCompleted);

        finished = true;
    }

    protected override void Awake()
    {
        base.Awake();
        characterProfiles = CharacterProfileBuilder.GetAll();
        characterPool.CreatePool(rawCharacterPref, initNum);
    }

    public Character GetCharacter(CharacterName name)
    {
        var profile = GetProfile(name);
        if (profile != null)
        {
            var chara = characterPool.GetObj();
            profile.LoadToCharacter(chara);
        }

        return Instantiate(rawCharacterPref);
    }

    public CharacterProfile GetProfile(CharacterName name)
    {
        for (int i = 0; i < characterProfiles.Length; i++)
        {
            if (characterProfiles[i].name == name)
            {
                return characterProfiles[i];
            }
        }

        return null;
    }
}
