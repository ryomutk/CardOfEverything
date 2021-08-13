using UnityEngine;
using Actor;

namespace CardSystem
{
    public interface ICardViewPoint
    {
        void OnInput(Card subject,Character target,InputArg arg);
        void AddCard(Card instance);
    }
}