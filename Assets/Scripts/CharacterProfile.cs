using UnityEngine;
using System;

namespace Actor
{
    [Serializable]
    public class CharacterProfile
    {
        [SerializeField] int hp = 30;
        [SerializeField] int strength = 50;
        [SerializeField] int dexterity = 30;
        [SerializeField] int diffence = 30;
        [SerializeField] int power = 16;
        
    }
}