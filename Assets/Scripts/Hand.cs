using UnityEngine;
using System.Collections.Generic;
using Actor;
using UnityEngine.UI;

namespace CardSystem
{
    [RequireComponent(typeof(LayoutField<Character>))]
    public class Hand : MonoBehaviour, ICardViewPoint
    {

        public List<Card> cardsInHand { get; private set; }
        public bool locked { get; set; }
        Card selectedCard;
        LayoutField<Card> place;

        void Start()
        {
            place = GetComponent<LayoutField<Card>>();
        }

        public virtual void OnInput(Card subject, Character target, InputArg arg)
        {
            if (arg.type == InputType.Click)
            {
                if (selectedCard == subject)
                {
                    UseCard(subject, target);
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

        bool UseCard(Card card, Character target)
        {
            if (locked)
            {
                return false;
            }

            if (card == selectedCard)
            {
                cardsInHand.Remove(card);
                place.Remove(card);
                card.Use(target);
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