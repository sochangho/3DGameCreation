using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GameData" , menuName = "CharacterData")]
public class CharactorData : ScriptableObject
{
    public string characterName;
    public string subscript;
    public float hp;
    public float damage;
    public float range;
    public float defence;
    public float speed;
    public int cost;
    public Charater charater;
    public Sprite sprite;
    
    
}
