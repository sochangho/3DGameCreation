using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBuff : Buff
{

    public override void BuffStart(AimObject parameter)
    {
        parameter.cur_hp += value;
        
        if(parameter.cur_hp > parameter.hp)
        {
            parameter.cur_hp = parameter.hp;

        }

    }


}
