using UnityEngine;
using Actor;
public enum PassiveName
{
    none
}

public enum PassiveType
{
    none
}

public abstract class PassiveEffect:ScriptableObject
{
    new public abstract PassiveName name{get;}
    public abstract PassiveType type{get;}
    public abstract void OnTurnEvent(TurnState state,Character target);
    public abstract void Disable(Character target);
}
