using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OponentTargetEffect : Effect , ITileCilckTrigger
{



    public int range;


    public void TileCilckTrigger(Node node, Player player)
    {
       Collider[]  colliders = Physics.OverlapSphere(node.transform.position, range);

        // 이 노드 위치에 파티클 생성;

       
       for(int i = 0; i < colliders.Length; i++)
       {
           
            IAttacked attacked = colliders[i].GetComponent<IAttacked>();
            
            if(attacked == null)
            {
                continue;
            }

            if(attacked.AttackedObjectType() == player.playertype)
            {
                continue;
            }

            

            AimObject aimObject = colliders[i].GetComponent<AimObject>(); 
            
            if(aimObject == null)
            {
                continue;
            }

            for(int j = 0; j< buffs.Count; j++)
            {
              

                aimObject.buffController.AddBuff(buffs[j].GetCloneBuff() , buffs[j].buffEffect);

            }

       }



    }

    public float GetEffectRenge()
    {
        return range;
    }
}
