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
         objects = GameSceneManager.Instance.ownPlayer.GetObjects();
        
        }
        else
        {
         objects = GameSceneManager.Instance.oponentPlayer.GetObjects();
        }

      
        gameObj.attackTarget = (AimObject)FindMinDistanceObj(objects);
       
    }




}
