using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRanceAttack : Attack
{

    override public void AttackTarget(AimObject target)
    {
        if(target == null)
        {
            return;
        }

        float damage = gameObj.GetComponent<IObjectInfo>().GetDamage();
        //target.Hit(gameObj);

    }
}
