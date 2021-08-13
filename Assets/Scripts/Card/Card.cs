using UnityEngine;
using Effects;
using Actor;
namespace CardSystem
{
    public abstract class Card : MonoBehaviour
    {
        public new CardName name { get { return profile.name; }}
        public abstract CharacterStatus statusRequirement{get;}
        public AbilityAttribute attribute { get { return profile.attribute; }}
        public int defaultWeight { get { return profile.defaultWeight; }}
        public Sprite thumbnail { get { return profile.thumbnail; }}
        public string flavorText { get { return profile.flavorText; }}
        public string summary { get { return profile.summary; }}

        [SerializeField] EffectName enterMotionId = EffectName.enter_card_motion;
        [SerializeField] EffectName exitMotionId = EffectName.exit_card_motion;
        [SerializeField] EffectName selectedMotionId = EffectName.selected_card_motion;
        [SerializeField] EffectName useMotionId = EffectName.use_card_motion;
        IVisualEffect enterMotion;
        IVisualEffect exitMotion;
        IVisualEffect selectedMotion;
        IVisualEffect useMotion;



        CardViewProfile profile;
        CardActionBase action;

        protected virtual void Start()
        {
            enterMotion = EffectServer.instance.GetGUIMotion(enterMotionId,gameObject);
            exitMotion = EffectServer.instance.GetGUIMotion(exitMotionId,gameObject);
            selectedMotion = EffectServer.instance.GetGUIMotion(selectedMotionId,gameObject);
            useMotion = EffectServer.instance.GetGUIMotion(useMotionId,gameObject);
        }

        public void Initialize(CardViewProfile profile,CardActionBase action)
        {
            this.profile = profile;
            this.action = action;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {

        }

        public void Select()
        {

        }

        public string GetDetail(Character master)
        {
            return action.GetDetail(master);
        }

        public void Use(Character target)
        {
            action.Execute(target);
        }

    }
}