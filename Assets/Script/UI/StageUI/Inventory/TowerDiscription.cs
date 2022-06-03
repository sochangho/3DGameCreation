using UnityEngine;
using TMPro;
using System.Collections;

public class TowerDiscription : CardDiscription
{


    public TextMeshProUGUI hpText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenceText;
    public TextMeshProUGUI rangeText;

    override public void Set(ScriptableObject scriptable)
    {

        TowerData data = (TowerData)scriptable;


        nameText.text = data.towerName;
        hpText.text = data.hp.ToString();
        attackText.text = data.damage.ToString();
        defenceText.text = data.defence.ToString();
        rangeText.text = data.range.ToString();
    }

}
