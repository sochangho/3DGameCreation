using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryCard : MonoBehaviour
{

    public ScriptableObject data;
    public Image image;
    public Button button;


    public void Setting(ScriptableObject data, UnityAction clickAction)
    {
        this.data = data;
        if (data is CharactorData)
        {
            CharactorData charactorData = (CharactorData)this.data;
            image.sprite = charactorData.sprite;

        }
        else if(data is EffectData)
        {
            EffectData effectData = (EffectData)this.data;
            image.sprite = effectData.sprite;
        }

        
        button.onClick.AddListener(clickAction);
        
    }

}
