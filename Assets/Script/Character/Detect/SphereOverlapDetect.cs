using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereOverlapDetect : Detect
{
    private float detectRange;
    public SphereOverlapDetect(Charater obj , float detectRange)
    {
        this.gameObj = obj;
        this.detectRange = detectRange;
    }

    public override void OnDetect()
    {
       Collider[] colliders =  Physics.OverlapSphere(gameObj.transform.position, detectRange);
       List<IAttacked> objs = new List<IAttacked>();
       for(int i = 0; i < colliders.Length; i++)
        {            
            var character = colliders[i].GetComponent<IAttacked>();
            if (character != null)
            {
                 objs.Add(character);

            }
        }

        gameObj.attackTarget = (Charater)FindMinDistanceObj(objs);

    }
}
