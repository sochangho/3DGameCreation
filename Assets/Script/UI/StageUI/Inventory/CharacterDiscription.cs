
using UnityEngine;
using TMPro;
public class CharacterDiscription : CardDiscription
{

   
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenceText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI rangeText;

    public GameObject stat;

    public override void Set(ScriptableObject scriptable)
    {

        CharactorData data = (CharactorData)scriptable;

  
        nameText.text = data.characterName;
        hpText.text = data.hp.ToString();
        attackText.text = data.damage.ToString();
        defenceText.text = data.defence.ToString();
        speedText.text = data.speed.ToString();

        if (data.charater.GetComponent<FarAwayAttack>() != null)
        {
            rangeText.text = data.range.ToString();
        }
        else
        {
            rangeText.text = "근접 거리";
        }

        
    }
}
