using UnityEngine;

namespace CardSystem
{
    public class Card : MonoBehaviour
    {
        CardProfile profile;
        public CardName id{get{return profile.id;}}

        public void LoadProfile(CardProfile profile)
        {

        }
    }
}