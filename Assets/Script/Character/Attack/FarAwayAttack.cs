using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FarAwayAttack : Attack
{
    public GameObject  projectile;
    public Transform spwanPoint;


    public Action projectileAtion;

    public float speed;

    public float Speed
    {

        get
        { float v = speed;

            //���� ����????

            return v;
        }
    }



    override public void init(AimObject charater)
    {
        gameObj = charater;
        detect = new AllDectect(gameObj);
    }

    override public void AttackTarget(AimObject target)
    {
        //TODO : ȿ���߰� , ��ų , ���ݵ� , ObjectPooling �̿��ϱ�


        Vector3 pos = target.transform.position;

        pos.y = gameObj.transform.position.y;

        transform.LookAt(pos);

        Projectile pro = projectile.GetComponent<Projectile>();

        if(pro == null)
        {

            pro = projectile.AddComponent<Projectile>();
        }



        
        //������Ʈ Ǯ�� �ٲ۴�.
        var go = Instantiate(pro);
        go.transform.position = spwanPoint.position;
        ProjectileWay(go);
        go.projectileWayAction = projectileAtion;
        
        go.effectAction = (Collision collision) =>
        {
            AimObject aimObject = collision.gameObject.GetComponent<AimObject>();
            if (aimObject != null && aimObject.player.playertype != gameObj.player.playertype)
            {

                aimObject.Hit(gameObj);

                //��ų ����???
               
                //������Ʈ Ǯ�� �ٲ۴�.
                Destroy(go.gameObject);
            }
            else
            {

                //Destroy(go.gameObject);
                return;
            }


        };

        go.projectileAttack();

    }

    virtual public void ProjectileWay(Projectile projectile)
    {
       

    }
}
