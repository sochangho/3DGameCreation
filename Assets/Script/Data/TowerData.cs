using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "tower", menuName = "TowerData")]
public class TowerData : ScriptableObject
{
    public string towerName;
    public string subscript;
    public float hp;
    public float damage;
    public float defence;
    public float range;
    public float AttackDelayTime;
    public AimObject aimObject;
    public Sprite sprite;
}
