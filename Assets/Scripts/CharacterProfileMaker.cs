using UnityEngine;
using UnityEditor;
using Utility;
using CardSystem;
using Actor;

//カード向けのJSONを作るためのエディタ拡張
public class CharacterProfileMaker : EditorWindow
{
    CharacterProfile jsonProfile;
    GUILayoutOption[] options = new[] {
            GUILayout.Width (64),
            GUILayout.Height (64)};

    [MenuItem("Window/CardSystem/CharacterMaker")]
    static void Create()

    {
        GetWindow<CharacterProfileMaker>("CharacterMaker");
    }


    private void OnGUI()
    {
        if (jsonProfile == null)
        {
            jsonProfile = new CharacterProfile();
        }

        using (new GUILayout.VerticalScope())
        {
            using (new GUILayout.HorizontalScope())
            {
                jsonProfile.name = (CharacterName)EditorGUILayout.EnumPopup(jsonProfile.name);
                if (GUILayout.Button("読み込む"))
                {
                    jsonProfile = CharacterProfileBuilder.Get(jsonProfile.name);
                }
            }
            jsonProfile.hp = EditorGUILayout.IntField("hp",jsonProfile.hp);
            jsonProfile.strength = EditorGUILayout.IntField("hp",jsonProfile.hp);
            jsonProfile.dexterity = EditorGUILayout.IntField("hp",jsonProfile.hp);
            jsonProfile.diffence = EditorGUILayout.IntField("hp",jsonProfile.hp);
            jsonProfile.power = EditorGUILayout.IntField("hp",jsonProfile.hp);
            jsonProfile.mental = EditorGUILayout.IntField("hp",jsonProfile.hp);
            jsonProfile.magic = EditorGUILayout.IntField("hp",jsonProfile.hp);
            jsonProfile.casting = EditorGUILayout.IntField("hp",jsonProfile.hp);

            jsonProfile.enterMotionID = (EffectName)EditorGUILayout.EnumPopup("EnterMotion",jsonProfile.enterMotionID);
            jsonProfile.exitMotionID = (EffectName)EditorGUILayout.EnumPopup("ExitMotion",jsonProfile.exitMotionID);
            jsonProfile.deathMotionID = (EffectName)EditorGUILayout.EnumPopup("DeathMotion",jsonProfile.deathMotionID);
            jsonProfile.damageMotionID = (EffectName)EditorGUILayout.EnumPopup("DamageMotion",jsonProfile.damageMotionID);

            if (GUILayout.Button("生成"))
            {
                Write();
            }
        }
    }

    void Write()
    {
        var data = JsonHelper.GetData<CharacterProfile>(jsonProfile.name.ToString());

        if (data == null)
        {
            JsonHelper.SaveData<CharacterProfile>(jsonProfile, jsonProfile.name.ToString());
            return;
        }

        //オーバーライト未実装
        JsonHelper.SaveData<CharacterProfile>(jsonProfile, jsonProfile.name.ToString());

    }

}