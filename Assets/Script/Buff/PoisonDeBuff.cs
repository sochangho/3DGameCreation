using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDeBuff : Buff
{

    public float delayTime;
    private float curTime = 0;

    public override void Buffproceeding(AimObject parmeter)
    {
        
        if(Time.time < curTime + delayTime)
        {
            return;
        }

        curTime = Time.time;
        parmeter.cur_hp -= value;

        if(parmeter.cur_hp < 0)
        {
            parmeter.cur_hp = 0;
            parmeter.Die();

        }


    }



}
