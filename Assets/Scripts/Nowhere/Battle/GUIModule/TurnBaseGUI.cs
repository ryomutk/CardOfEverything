using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//Turnの中で動かしたいUIの、
//アップデートタイミングを保持し、
//GameManagerに登録登録
[System.Serializable]
public class TurnBaseGUI
{
    [SerializeField] List<TurnState> _updateOn;
    [SerializeField] GUIEffectName _baseEffectName;
    List<TurnState> updateOn { get { return _updateOn; } set { updateOn = value; } }

    GUIEffectName baseEffectName { get { return _baseEffectName; } set { _baseEffectName = value; } }
    Effects.IVisualEffect baseEffect;
    System.Action<TurnState> updateCallback;

    protected virtual void UpdateCallback(TurnState state)
    {
        if (updateOn.Contains(state))
        {
            GameManager.instance.RegisterGUIMotion(baseEffect);
        }
    }

    public void Activate()
    {
        BattleManager.instance.OnTurnEvent += updateCallback;
    }

    public void DisActivate()
    {
        BattleManager.instance.OnTurnEvent -= updateCallback;
    }

    public TurnBaseGUI Clone(MonoBehaviour target)
    {
        this.baseEffect = EffectServer.instance.GetGUIMotion(baseEffectName, target);
        updateCallback = (x) => UpdateCallback(x);
        
        return (TurnBaseGUI)MemberwiseClone();
    }
}