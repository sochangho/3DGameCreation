using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarAwayAttack : Attack
{
    public Projectile  projectile;
    public Transform spwanPoint;
    override public void AttackTarget(IAttacked target)
    {



        //TODO : ȿ���߰� , ��ų , ���ݵ� , ObjectPooling �̿��ϱ� 

        Vector3 startDir = spwanPoint.position;
        Vector3 endDir = gameObj.attackTarget.transform.position;
        Vector3 direction = startDir - endDir;

         var go = Instantiate(projectile);

        go.projectileWayAction = () =>
        {
            go.transform.Translate(direction.normalized * gameObj.Speed * Time.deltaTime);
        };

        go.effectAction = (Collision collision) =>
        {
            collision.gameObject.GetComponent<IAttacked>().Hit(gameObj);
            Destroy(go);
        };
        go.projectileAttack();
    }

    virtual public void ProjectileWay()
    {
       

    }
}
