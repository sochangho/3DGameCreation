using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameData", menuName = "EffectData")]
public class EffectData : ScriptableObject
{

    public string effectName;
    public string subscrips;
    public int cost;
    public Effect effect;
    public Sprite sprite;


}
