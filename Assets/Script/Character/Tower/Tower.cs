using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : AimObject
{

    public TowerState towerState;
    public Transform rotationHead;

    public Detect detect;
    public Attack attack;

    public float attackdelayTime = 0.3f;
    private float curTime = 0;



    public float AttackDelayTime
    {
        get
        {
            float v = attackdelayTime;
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


    public void Awake()
    {
        detect = new SphereOverlapDetect(this, range);
        attack = GetComponent<Attack>();
        attack.init(this);
    }



    override public void Attack()
    {
        if (attackTarget == null)
        {
            return;
        }


        attack.AttackTarget(attackTarget);
    }



    public override void Hit(AimObject damage)
    {
        if (damage == null)
        {
            return;
        }



        cur_hp -= (damage.Damage - damage.Damage * Defence / 100);


        if (cur_hp <= 0)
        {

            cur_hp = 0;


            if(towerState != TowerState.Die)
            {
                towerState = TowerState.Die;
                player.RemoveCharacter(this);
                damage.attackTarget = null;

            }       
        }
    }

    private void Update()
    {

        if (towerState == TowerState.Detect)
        {

            detect.OnDetect();

            if (attackTarget != null)
            {
                towerState = TowerState.Attack;
                curTime = Time.time;
            }


        }
        else if (towerState == TowerState.Attack)
        {

           

            if (attackTarget == null)
            {
                towerState = TowerState.Detect;
            }
            else
            {


                rotationHead.LookAt(attackTarget.transform);

                if (Time.time < curTime + AttackDelayTime)
                {
                    
                    
                    return;
                }
               
                curTime = Time.time;
                Attack();
            }

        }
        else if (towerState == TowerState.Die)
        {

            // StopAI();

        }
    }






}
