using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBuff : Buff
{

    public override Buff GetCloneBuff()
    {


        HpBuff hpBuff = new HpBuff();
        hpBuff.value = this.value;
        hpBuff.buffName = this.buffName;
        hpBuff.buffType = this.buffType;
        hpBuff.duration = this.duration;


        return hpBuff;
    }




    public override void BuffStart(AimObject parameter)
    {
        parameter.cur_hp += value;
        parameter.HpImgSet();
        if(parameter.cur_hp > parameter.hp)
        {
            parameter.cur_hp = parameter.hp;

        }

    }


}
