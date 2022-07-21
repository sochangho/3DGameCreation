using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tower : AimObject
{

    public TowerState towerState;
    public Transform rotationHead;

    public Detect detect;
    public Attack attack;

    public float attackdelayTime = 0.3f;
    private float curTime = 0;

   // public TowerData data;





   override public void Awake()
    {
        TowerInit();
        base.Awake();
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



        cur_hp -= (damage.damage - damage.damage * defence / 100);

        base.Hit(damage);
        if (cur_hp <= 0)
        {

            cur_hp = 0;


            if(towerState != TowerState.Die)
            {
                towerState = TowerState.Die;
                player.RemoveCharacter(this);
                damage.attackTarget = null;
                Die();
            }       
        }
    }

    public override void Die()
    {
        if (GameSceneManager.Instance.is_gameEnd)
        {
            return;
        }
        GameSceneManager.Instance.is_gameEnd = true;
        ParameterHelper parameterHelper = new ParameterHelper();
        parameterHelper.objList.Add(this.player);
        parameterHelper.objList.Add(this);
       
        EventManager.Emit("GameEnd", parameterHelper);
    }


    private void TowerInit()
    {
        TowerData towerdata = (TowerData)data;
        if (towerdata != null)
        {
            hp = towerdata.hp;
            damage = towerdata.damage;
            range = towerdata.range;
            defence = towerdata.defence;
            attackdelayTime = towerdata.AttackDelayTime;
        }

        cur_hp = hp;
    }

    private void Update()
    {
        buffController.BuffTimer();
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

                Vector3 lookPos = new Vector3(attackTarget.transform.position.x, rotationHead.transform.position.y, attackTarget.transform.position.z);

                rotationHead.LookAt(lookPos);

                if (Time.time < curTime + attackdelayTime)
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
