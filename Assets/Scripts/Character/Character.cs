using UnityEngine;
using Effects;
using Trigger;
using System.Collections.Generic;
using CardSystem;
using System.Collections;

namespace Actor
{
    public abstract class Character : MonoBehaviour
    {
        public List<int> actionWeightList { get; set; }
        CharacterStatus _status = new CharacterStatus();

        /// <summary>
        /// 立ち絵を表示するためのレンダラ。
        /// なければnull
        /// </summary>
        public SpriteRenderer viewRenderer { get; private set; }
        public CharacterStatus Status { get { return _status; } }
        public List<CardSystem.CardViewProfile> cardList { get; protected set; }
        public event CharacterAction OnEnter;
        public event CharacterAction OnExit;

        //キャラのステータス変更を行いたいときの受付窓口。
        /// <summary>
        /// actionWeightArrayはそのまま変更かなー
        /// </summary>
        public virtual void ModifyStatus(CharacterStates target, int amount)
        {
            Status.Modify(this, target, amount);
        }

        public virtual void Enter()
        {
            OnEnter(this);
        }

        public virtual void Exit()
        {
            OnExit(this);
        }

        void OnDisable()
        {
            OnEnter = null;
            OnExit = null;
        }


        protected void Start()
        {
            viewRenderer = GetComponent<SpriteRenderer>();
            BattleManager.instance.OnBattleEvent += (x) => OnBattleEvent(x);
        }

        void OnBattleEvent(BattleState state)
        {
            if (state == BattleState.dataInitialize)
            {
                var task = new InteraptTask();
                BattleManager.instance.RegisterInterapt(task);
            }
        }

        IEnumerator PrepareForBattle(InteraptTask task)
        {
            InitActionWeight();
            //今はまだ特に意味なし。重い処理がないので
            yield return null;
            task.finished = true;
        }

        void InitActionWeight()
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                actionWeightList.Add(cardList[i].defaultWeight);
            }
        }

        public virtual void AddCard(CardSystem.CardName name)
        {
            var profile = CardServer.instance.GetProfile(name);
            cardList.Add(profile);
            actionWeightList.Add(profile.defaultWeight);
        }

        //カードを選ぶ。その後はSessionに任せる
        public virtual bool RemoveCard(CardSystem.CardName name, bool removeAll = false)
        {
            bool removeFlag = false;
            for (int i = 0; i < cardList.Count; i++)
            {
                if (cardList[i].name == name)
                {
                    cardList.RemoveAt(i);
                    actionWeightList.RemoveAt(i);

                    if (!removeAll)
                    {
                        return true;
                    }

                    removeFlag = true;
                }
            }

            return removeFlag;
        }

        public virtual CardName PickCard()
        {
            if (actionWeightList.Count != cardList.Count)
            {
                throw new System.Exception("actionWeight and cardCount is not same");
            }

            int wholeWeight = 0;
            for (int i = 0; i < cardList.Count; i++)
            {
                wholeWeight += actionWeightList[i];
            }

            var lottely = UnityEngine.Random.Range(0, wholeWeight);

            for (int i = 0; i < actionWeightList.Count; i++)
            {
                lottely -= actionWeightList[i];

                if (lottely < 0)
                {
                    return cardList[i].name;
                }
            }


            throw new System.Exception("Something is wrong");
        }
    }
}