using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAwayAttack : Attack
{
    public Projectile  projectile;

    override public void AttackTarget(IAttacked target)
    {

        projectile.projectileWayAction = ProjectileWay;

        //TODO : ȿ���߰� , ��ų , ���ݵ� , ObjectPooling �̿��ϱ� 


    }

    virtual public void ProjectileWay()
    {


    }
}
