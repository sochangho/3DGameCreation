using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDectect : Detect
{
    public AllDectect(AimObject obj)
    {
        this.gameObj = obj;
    }

    public override void OnDetect()
    {
        List<IAttacked> objects;
        if(((AimObject)gameObj).player.playertype == PlayerType.Oponent)
        {
         objects = GameSceneManager.Instance.ownPlayer.GetObjects<IAttacked>();
        
        }
        else
        {
         objects = GameSceneManager.Instance.oponentPlayer.GetObjects<IAttacked>();
        }

      
        gameObj.attackTarget = (AimObject)FindMinDistanceObj(objects);
       
    }




}
