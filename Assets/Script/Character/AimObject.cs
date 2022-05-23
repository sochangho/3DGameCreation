﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimObject : MonoBehaviour,IAttacked,IObjectInfo
{
    
    public AimObject attackTarget;
    public Player player;
    public float hp;
    public float cur_hp = 100;
    public float damage;
    public float defence;
    public float range;
    public int ID { get; set; }

    public GameObjType type;


    public Coroutine aiRoutin;

    readonly public BuffController buffController = new BuffController();

    void Awake()
    {
        buffController.BuffTarget(this);

    }


    public float Defence
    {
        get
        {
            float v = defence;
            List<Buff> buffs = buffController.GetBuffs();

            foreach (Buff buff in buffs)
            {
                if (buff is DefenceBuff)
                {

                    v += buff.value;
                }
            }
            return v;
        }
    }

    public float Damage
    {
        get
        {
            float v = damage;
            List<Buff> buffs = buffController.GetBuffs();

            foreach (Buff buff in buffs)
            {
                if (buff is DamageBuff)
                {

                    v += buff.value;
                }
            }
            return v;
        }



    }




    public float Range
    {

        get
        {
            float v = range;
            List<Buff> buffs = buffController.GetBuffs();

            foreach (Buff buff in buffs)
            {
                if (buff is AttackDelayBuff)
                {

                    v += buff.value;
                }
            }
            return v;
        }


    }


    public void StopAI()
    {
        if (aiRoutin != null)
        {

            StopCoroutine(aiRoutin);
        }
    }

    virtual public void Attack() { }
    virtual public void Hit(AimObject damage){}

    virtual public void Die() { }


    public PlayerType AttackedObjectType()
    {

        return player.playertype;
    }

    public float GetDamage()
    {

        return damage;
    }
    public float GetHp()
    {
        return hp;

    }
}
