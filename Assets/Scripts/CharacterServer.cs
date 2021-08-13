using UnityEngine;
using Actor;
using Utility;

public class CharacterServer : Singleton<CharacterServer>
{
    CharacterProfile[] characterProfiles;
    [SerializeField] Character rawCharacterPref;
    Utility.ObjPool.InstantPool<Character> characterPool;
    [SerializeField] int initNum = 10;

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

    CharacterProfile GetProfile(CharacterName name)
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
