using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Projectile : MonoBehaviour
{
    public Action effectAction;
    public Action projectileWayAction;
    public Coroutine projectileRoutin;
    private void OnEnable()
    {
      projectileRoutin = StartCoroutine(ProjectileWayRoutin());
    }

    private void OnDisable()
    {
        StopCoroutine(projectileRoutin);
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
        if(collision.gameObject.GetComponent<IAttacked>() != null)
        {
            if (effectAction != null)
            {
                effectAction();
            }

        }
    }

}
