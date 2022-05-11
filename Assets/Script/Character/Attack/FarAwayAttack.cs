using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAwayAttack : Attack
{
    public Projectile  projectile;

    override public void AttackTarget(IAttacked target)
    {

        projectile.projectileWayAction = ProjectileWay;

        //TODO : 효과추가 , 스킬 , 공격등 , ObjectPooling 이용하기 


    }

    virtual public void ProjectileWay()
    {


    }
}
