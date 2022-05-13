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

            //버프 설정????

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
        //TODO : 효과추가 , 스킬 , 공격등 , ObjectPooling 이용하기


        Vector3 pos = target.transform.position;

        pos.y = gameObj.transform.position.y;

        transform.LookAt(pos);

        Projectile pro = projectile.GetComponent<Projectile>();

        if(pro == null)
        {

            pro = projectile.AddComponent<Projectile>();
        }



        
        //오브젝트 풀로 바꾼다.
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

                //스킬 버프???
               
                //오브젝트 풀로 바꾼다.
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
