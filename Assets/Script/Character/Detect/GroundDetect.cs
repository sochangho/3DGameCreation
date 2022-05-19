using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : Detect
{
    public GroundDetect(AimObject obj)
    {
        this.gameObj = obj;
    }

    public override void OnDetect()
    {
        List<IAttacked> objects;
        if (gameObj.player.playertype == PlayerType.Oponent)
        {
          objects = GameSceneManager.Instance.ownPlayer.GetObjects<IAttacked>();
        }
        else
        {
          objects = GameSceneManager.Instance.oponentPlayer.GetObjects<IAttacked>();
        }

        List<IAttacked> groundObjects = new List<IAttacked>();

        foreach(IAttacked obj in objects)
        {
            if(((AimObject)obj).type == GameObjType.Ground)
            {
                groundObjects.Add(obj);

            }

        }

        objects = groundObjects;

        gameObj.attackTarget = (AimObject)FindMinDistanceObj(objects);
    }
}
