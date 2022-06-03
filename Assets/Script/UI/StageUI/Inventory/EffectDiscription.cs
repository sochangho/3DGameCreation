
using UnityEngine;

public class EffectDiscription : CardDiscription
{

    public override void Set(ScriptableObject scriptable)
    {
        EffectData effectData = (EffectData)scriptable;

        nameText.text = effectData.effectName.ToString();
        discripstionText.text = effectData.subscrips.ToString();

    }


}
