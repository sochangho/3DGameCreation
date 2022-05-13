using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : AimObject
{

    public TowerState towerState;

    public Transform rotationHead;

    public Detect detect;
    public Attack attack;

    public float attackdelayTime;
    private float curTime = 0;

    public void Awake()
    {
        detect = new SphereOverlapDetect(this, range);
        attack = GetComponent<Attack>();
    }


    public void Start()
    {
        
        

    }


    public void Attack()
    {

    }

    IEnumerator TowerBehavior()
    {

        while (true)
        {
            if(towerState == TowerState.Detect)
            {

                 detect.OnDetect();

                if (attackTarget != null)
                {
                    towerState = TowerState.Attack;
                }


            }else if(towerState == TowerState.Attack)
            {

                if(attackTarget == null)
                {
                    towerState = TowerState.Detect;
                }


                if( Time.time < curTime + AttackDelayTime)
                {
                    continue;
                }

                curTime = Time.time;
                Attack();


            }else if(towerState == TowerState.Die)
            {



            }


            yield return null;
        }


    }




}
