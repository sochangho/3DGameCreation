using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightAttack : FarAwayAttack
{
    public override void ProjectileWay()
    {

        //TODO : �������󰡱� 

        Vector3 startDir = spwanPoint.position;
        Vector3 endDir = gameObj.attackTarget.transform.position;
        Vector3 direction = startDir - endDir;


        projectile.projectileWayAction = () =>
        {




        }


     

    }
}
