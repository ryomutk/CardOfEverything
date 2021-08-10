using UnityEngine;
using System.Collections.Generic;
using Utility.ObjPool;


namespace CardSystem
{
    public class CardServer : MonoBehaviour
    {
        List<CardProfile> profileList;
        [SerializeField] Card rawCardPref;
        InstantPool<Card> rawCardPool;

        public Card GetCard(CardName id)
        {
            var profile = GetProfile(id);
            if(profile != null)
            {
                var instance = rawCardPool.GetObj();
                instance.LoadProfile(profile);

                return instance;
            }
            //こいつは…バグカード的な?

            return rawCardPref;
        }


        CardProfile GetProfile(CardName id)
        {
            for (int i = 0; i < profileList.Count; i++)
            {
                if (profileList[i].id == id)
                {
                    return profileList[i];
                }
            }

            return null;
        }

    }
}