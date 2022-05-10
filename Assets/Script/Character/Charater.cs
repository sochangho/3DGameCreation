using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charater : MonoBehaviour , IAttacked
{
    public float hp;
    public float defence;
    public float range;
    public float damage;
    public float speed;
    public float attackDelayTime;
    private float attackTime = 0;

    public Player player;
    public Charater attackTarget;

    readonly public BuffController buffController = new BuffController();

    public float Defence
    {
        get
        {
            float v = defence;
            List<Buff> buffs = buffController.GetBuffs();
            
            foreach(Buff buff in buffs)
            {
                if(buff is DefenceBuff)
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
            float v = defence;
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


    public float AttackDelayTime
    {
        get
        {
            float v = defence;
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


    public float Range
    {

        get
        {
            float v = defence;
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

    public float Speed
    {

        get
        {
            float v = defence;
            List<Buff> buffs = buffController.GetBuffs();

            foreach (Buff buff in buffs)
            {
                if (buff is SeepdBuff)
                {

                    v += buff.value;
                }
            }
              
            return v;
        }

    }

    public CharaterState state;
    public CharaterType type;



    public void Hit(float damage)
    {
        
    }

    IEnumerator CharacterRoutin()
    {
        while (true)
        {
            if(state == CharaterState.Detect)
            {


            }
            else if(state == CharaterState.Trace)
            {


            }
            else
            {



            }

            yield return null;
        }


    }

}
