using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FarAwayAttack : Attack
{
    public Projectile projectile;
    public ProjectileParticle projectileParticle;



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

        //오브젝트 풀로 바꾼다.
        var go = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectile.name);

        if(go == null)
        {

            ObjectPooling.ObjectPoolingManager.Instance
                .AddObjects(projectile.name, projectile.gameObject, 5);
            go = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectile.name);
        }


        Projectile pro = go.GetComponent<Projectile>();
        pro.transform.position = spwanPoint.position;
        ProjectileWay(pro);
        pro.projectileWayAction = projectileAtion;
        
        if(gameObj.player.playertype == PlayerType.Oponent)
        {
            pro.gameObject.layer = 13;
        }else if(gameObj.player.playertype == PlayerType.Own)
        {
            pro.gameObject.layer = 12;
        }


      
            pro.effectAction = (Collision collision) =>
            {
                AimObject aimObject = collision.gameObject.GetComponent<AimObject>();
                if (aimObject != null && aimObject.player.playertype != gameObj.player.playertype)
                {

                    aimObject.Hit(gameObj);

                    //스킬 버프???
                    GameObject particle = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectileParticle.name);
                    particle.transform.position = collision.contacts[0].point;
                    particle.transform.LookAt(collision.contacts[0].normal);

                    pro.GetComponent<ObjectPooling.ObjectPool>().GameObjDead();
                    pro.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

         
            };


        pro.groundAction = (Collision collision) => {

            if (collision.gameObject.tag == "Ground")
            {
               
                GameObject particle = ObjectPooling.ObjectPoolingManager.Instance.ObjectUse(projectileParticle.name);
                particle.transform.position = collision.contacts[0].point;
                particle.transform.LookAt(collision.contacts[0].normal);

                pro.GetComponent<ObjectPooling.ObjectPool>().GameObjDead();
                pro.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

        };

        
        pro.projectileAttack();

    }

    virtual public void ProjectileWay(Projectile projectile)
    {
       

    }
}
