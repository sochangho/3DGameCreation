using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Projectile : MonoBehaviour
{
    public Action<Collision> effectAction;
    public Action projectileWayAction;
    public Coroutine projectileRoutin;
    private void OnEnable()
    {
      
    }

    private void OnDisable()
    {
        StopCoroutine(projectileRoutin);
    }

    public void projectileAttack() {

        projectileRoutin = StartCoroutine(ProjectileWayRoutin());

    }

    IEnumerator ProjectileWayRoutin()
    {

        while (true)
        {
            if(projectileWayAction != null)
            {
                projectileWayAction();
            }

            yield return null;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        IAttacked attacked = collision.gameObject.GetComponent<IAttacked>();
       

        if (attacked != null && effectAction != null )
        {
          
            effectAction(collision);
            
        }
    }

}
