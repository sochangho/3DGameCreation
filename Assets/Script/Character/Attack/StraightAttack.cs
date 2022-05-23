using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightAttack : FarAwayAttack
{
    public override void ProjectileWay(Projectile projectile)
    {

        //TODO : 직선날라가기 

        Vector3 startDir = spwanPoint.position;
        Vector3 endDir = gameObj.attackTarget.transform.position;
        Vector3 direction = endDir - startDir;


        projectileAtion = () =>
        {

            //projectile.transform.Translate(direction.normalized * Speed * Time.deltaTime); };
            projectile.GetComponent<Rigidbody>().AddForce(direction.normalized * Speed , ForceMode.Force);

        };
    }
}
