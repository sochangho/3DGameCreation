using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDectect : Detect
{
    public AllDectect(Charater obj)
    {
        this.gameObj = obj;
    }

    public override void OnDetect()
    {
        List<IAttacked> objects;
        if(((Charater)gameObj).player.playertype == PlayerType.Oponent)
        {
         objects = GameSceneManager.Instance.ownPlayer.GetObjects();
        
        }
        else
        {
         objects = GameSceneManager.Instance.oponentPlayer.GetObjects();
        }

      
        gameObj.attackTarget = (Charater)FindMinDistanceObj(objects);
       
    }




}
