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
            jsonProfile.defaultThumbnail = (Sprite)EditorGUILayout.ObjectField(jsonProfile.defaultThumbnail, typeof(Sprite), false, options);
            jsonProfile.hp = EditorGUILayout.IntField("hp", jsonProfile.hp);
            jsonProfile.strength = EditorGUILayout.IntField("strength", jsonProfile.strength);
            jsonProfile.dexterity = EditorGUILayout.IntField("dex", jsonProfile.dexterity);
            jsonProfile.diffence = EditorGUILayout.IntField("dif", jsonProfile.diffence);
            jsonProfile.power = EditorGUILayout.IntField("power", jsonProfile.power);
            jsonProfile.mental = EditorGUILayout.IntField("mental", jsonProfile.mental);
            jsonProfile.magic = EditorGUILayout.IntField("magic", jsonProfile.magic);
            jsonProfile.casting = EditorGUILayout.IntField("casting", jsonProfile.casting);

            jsonProfile.enterMotionID = (ObjEffectName)EditorGUILayout.EnumPopup("EnterMotion", jsonProfile.enterMotionID);
            jsonProfile.exitMotionID = (ObjEffectName)EditorGUILayout.EnumPopup("ExitMotion", jsonProfile.exitMotionID);
            jsonProfile.deathMotionID = (ObjEffectName)EditorGUILayout.EnumPopup("DeathMotion", jsonProfile.deathMotionID);
            jsonProfile.damageMotionID = (TextEffectName)EditorGUILayout.EnumPopup("DamageMotion", jsonProfile.damageMotionID);

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