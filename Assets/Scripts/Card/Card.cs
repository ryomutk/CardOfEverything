using UnityEngine;
using Effects;
using Actor;
namespace CardSystem
{
    public abstract class Card : MonoBehaviour
    {
        public new CardName name { get { return profile.name; } }
        public abstract CharacterStates statusRequirement { get; }
        public AbilityAttribute attribute { get { return profile.attribute; } }
        public int defaultWeight { get { return profile.defaultWeight; } }
        public Sprite thumbnail { get { return profile.thumbnail; } }
        public string flavorText { get { return profile.flavorText; } }
        public string summary { get { return profile.summary; } }

        [SerializeField] CardFormatter formatter;


        public event System.Action<Card, CardEventName> onCardEvent;



        //Characterと違ってこれはゲーム間でもあまり変わらないので
        //一つ一つではなくProfileを入力してもらう
        public CardViewProfile profile { get; set; }
        CardActionBase action;

        protected virtual void Start()
        {
            //null回避
            onCardEvent += (x,y) => {};
        }

        public void Initialize(CardViewProfile profile, CardActionBase action)
        {
            this.profile = profile;
            this.action = action;
            profile.LoadToCard(this);
            
        }

        public void Enter()
        {
            onCardEvent(this, CardEventName.entered);
        }

        public void Exit()
        {
            onCardEvent(this, CardEventName.exited);
        }

        public void Select(bool selected)
        {
            if (selected)
            {

                onCardEvent(this, CardEventName.selected);
            }
            else
            {
                onCardEvent(this,CardEventName.disSelected);
            }
        }

        public string GetDetail(Character master)
        {
            return action.GetDetail(master);
        }

        public void Use(Character target)
        {
            onCardEvent(this,CardEventName.used);
            action.Execute(target);
        }

        protected void OnDisable()
        {
            onCardEvent(this,CardEventName.disabled);
            onCardEvent = null;
        }

    }
}