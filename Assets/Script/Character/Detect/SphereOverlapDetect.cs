using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereOverlapDetect : Detect
{
    private float detectRange;
    public SphereOverlapDetect(Object obj , float detectRange)
    {
        this.gameObj = obj;
        this.detectRange = detectRange;
    }

    public override void OnDetect()
    {
       Collider[] colliders =  Physics.OverlapSphere(((GameObject)gameObj).transform.position, detectRange);

       for(int i = 0; i < colliders.Length; i++)
        {
            var attackTarget = colliders[i].GetComponent<IAttacked>();
            var character = colliders[i].GetComponent<Charater>();
            if (attackTarget != null && character != null
                //조건 
                )
            {
                // 거리 계산 

            }


        }

        
      
    }
}
