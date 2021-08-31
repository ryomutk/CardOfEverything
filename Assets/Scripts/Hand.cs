using UnityEngine;
using System.Collections.Generic;
using Actor;
using UnityEngine.UI;
using Utility;

namespace CardSystem
{
    [RequireComponent(typeof(LayoutField<Card>))]
    public class Hand : Singleton<Hand>, ICardViewPoint
    {

        public List<Card> cardsInHand { get; private set; }
        public bool locked { get; set; }
        Card selectedCard;
        LayoutField<Card> place;
        public Character master { get; private set; }

        //SystemInitializeで自動的に使用者を割り当てるか
        //複数Player非対応
        [SerializeField] bool autoSetMaster = true;

        void Start()
        {
            place = GetComponent<LayoutField<Card>>();
            if (autoSetMaster)
            {
                GameManager.instance.onGameEvent += (x) =>
                {
                    if(x == GameState.systemInitialize)
                    {
                        if(PlayerManager.instance.playerNum == 1)
                        {
                            master = PlayerManager.instance.GetPlayer(0);    
                        }
                        else
                        {
                            throw new System.NotImplementedException();
                        }
                    }
                };
            }
        }

        public void SetMaster(Character master)
        {
            this.master = master;
        }



        public virtual void OnInput(Card subject, InputArg arg)
        {
            if (arg.type == InputType.Click)
            {
                if (selectedCard == subject)
                {
                    UseCard(subject);
                }
            }
        }

        public virtual void AddCard(Card instance)
        {
            place.Place(instance);
        }


        public Hand()
        {
            cardsInHand = new List<Card>();
        }

        public bool UseCard(Card card, Character target = null)
        {
            if (locked)
            {
                return false;
            }

            if (card == selectedCard)
            {
                cardsInHand.Remove(card);
                place.Remove(card);

                if (target != null)
                {
                    card.Use(target);
                }
                else
                {
                    card.Use(master);
                }


                return true;
            }
            else
            {
                card.Select(true);
                selectedCard.Select(false);
                selectedCard = card;

                return false;
            }

        }

    }
}