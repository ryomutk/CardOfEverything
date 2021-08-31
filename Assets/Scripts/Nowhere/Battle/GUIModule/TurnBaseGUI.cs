using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//TurnEventを使って更新する方式のGUI。
//登録したEffectが登録したTurnEvent毎に更新される
[System.Serializable]
public class TurnBaseGUI
{
    [SerializeField] List<TurnState> _updateOn;
    [SerializeField] GUIEffectName _baseEffectName;
    List<TurnState> updateOn { get { return _updateOn; } set { updateOn = value; } }

    GUIEffectName baseEffectName { get { return _baseEffectName; } set { _baseEffectName = value; } }
    Effects.IVisualEffect baseEffect;

    protected virtual void UpdateCallback(TurnState state)
    {
        if (updateOn.Contains(state))
        {
            GameManager.instance.RegisterGUIMotion(baseEffect);
        }
    }

    public TurnBaseGUI Clone(MonoBehaviour target)
    {
        this.baseEffect = EffectServer.instance.GetGUIMotion(baseEffectName, target);
        BattleManager.instance.OnTurnEvent += (x) => UpdateCallback(x);
        
        return (TurnBaseGUI)MemberwiseClone();
    }
}