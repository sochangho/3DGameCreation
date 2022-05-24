using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDeBuff : Buff
{

    public float delayTime;
    private float curTime = 0;


    public override Buff GetCloneBuff()
    {


        PoisonDeBuff poison = new PoisonDeBuff();
        poison.value = this.value;
        poison.buffName = this.buffName;
        poison.buffType = this.buffType;
        poison.duration = this.duration;
        poison.delayTime = this.delayTime;
        poison.curTime = 0;
        return poison;
    }




    public override void Buffproceeding(AimObject parmeter)
    {
       


        if (Time.time < curTime + delayTime)
        {
            return;
        }

        
        
        curTime = Time.time;
        parmeter.cur_hp -= value;

        parmeter.HpImgSet();

        if(parmeter.cur_hp < 0)
        {
            parmeter.cur_hp = 0;
            parmeter.Die();

        }


    }



}
