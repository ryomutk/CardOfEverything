using UnityEngine;
using System;
using System.Linq;

namespace Actor
{
    [Serializable]
    public class CharacterProfile
    {
        [SerializeField] CharacterName _name;
        [SerializeField] int _hp = 30;
        [SerializeField] int _strength = 50;
        [SerializeField] int _dexterity = 30;
        [SerializeField] int _diffence = 30;
        [SerializeField] int _power = 16;
        [SerializeField] int _mental = 32;
        [SerializeField] int _magic = 50;
        [SerializeField] int _casting = 50;
        [SerializeField] CardSystem.CardName[] _cards;

        //誰にでも共通してそんざいしているモーション
        [SerializeField] ObjEffectName _enterMotionID;
        [SerializeField] ObjEffectName _exitMotionID;
        [SerializeField] ObjEffectName _deathMotionID;
        [SerializeField] TextEffectName _damageMotionID;

        public Sprite defaultThumbnail;
        public CharacterName name { get { return _name; } set { _name = value; } }
        public int hp { get { return _hp; } set { _hp = value; } }
        public int strength { get { return _strength; } set { _strength = value; } }
        public int dexterity { get { return _dexterity; } set { _dexterity = value; } }
        public int diffence { get { return _diffence; } set { _diffence = value; } }
        public int power { get { return _power; } set { _power = value; } }
        public int mental { get { return _mental; } set { _mental = value; } }
        public int magic { get { return _magic; } set { _magic = value; } }
        public int casting { get { return _casting; } set { _casting = value; } }
        public CardSystem.CardName[] cards { get { return _cards; } set { _cards = value; } }
        public ObjEffectName enterMotionID { get { return _enterMotionID; } set { _enterMotionID = value; } }
        public ObjEffectName exitMotionID { get { return _exitMotionID; } set { _exitMotionID = value; } }
        public ObjEffectName deathMotionID { get { return _deathMotionID; } set { _deathMotionID = value; } }
        public TextEffectName damageMotionID { get { return _damageMotionID; } set { _damageMotionID = value; } }


        //今後Character側の初期化手法などが変わることも考えて、その際にここで対応できるようにこちら側で
        //ロード用メソッドを用意。消して公開が面倒だったわけではない。
        //さらに、キャラのイベントにここで行動の呼び出しを書いておくことで
        //Motion何があるかをCharaが把握していなくてもよいようにする
        //ロード手法までデータに書くの、ちょっといいかもしれない。
        public void LoadToCharacter(Character target)
        {
            target.Status.Initialize();
            target.Status.statusDictionary[CharacterStates.hp] = _hp;
            target.Status.statusDictionary[CharacterStates.strength] = _strength;
            target.Status.statusDictionary[CharacterStates.dexterity] = _dexterity;
            target.Status.statusDictionary[CharacterStates.diffence] = _diffence;
            target.Status.statusDictionary[CharacterStates.power] = _power;
            target.Status.statusDictionary[CharacterStates.mental] = _mental;
            target.Status.statusDictionary[CharacterStates.magic] = _magic;
            target.Status.statusDictionary[CharacterStates.casting] = _casting;

            var enter = EffectServer.instance.GetObjEffect(_enterMotionID, target.gameObject);
            target.OnEnter += (target) => BattleManager.instance.RegisterFX(enter);

            var exit = EffectServer.instance.GetObjEffect(_exitMotionID, target.gameObject);
            target.OnEnter += (target) => BattleManager.instance.RegisterFX(exit);

            var death = EffectServer.instance.GetObjEffect(_deathMotionID, target.gameObject);
            target.Status.OnDeath += (target) => BattleManager.instance.RegisterFX(death);

            target.Status.OnModify += (x, y, z) => DamageAction(x, y, z);

            LoadProfiles(target);

            if(target.viewRenderer != null)
            {
                target.viewRenderer.sprite = defaultThumbnail;
            }
        }

        void DamageAction(Character target, CharacterStates states, int amount)
        {
            if (states == CharacterStates.hp && amount < 0)
            {
                var damage = EffectServer.instance.GetTextEffect(_damageMotionID, target.transform, amount.ToString());
                BattleManager.instance.RegisterFX(damage);
            }
        }

        void LoadProfiles(Character target)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                var name = cards[i];
                var profile = CardServer.instance.GetProfile(name);

                target.cardList.Add(profile);
            }
        }
    }
}