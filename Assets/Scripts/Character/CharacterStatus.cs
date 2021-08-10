using System;

namespace Actor
{
    [Flags]
    public enum CharacterStatus
    {
        none,
        hp = 1,
        strength = 2,
        dexterity = 4,
        diffence = 8,
        power = 16,
        casting = 32
    }
}