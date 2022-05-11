using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetect : Detect
{
    public GroundDetect(Charater obj)
    {
        this.gameObj = obj;
    }

    public override void OnDetect()
    {
        List<IAttacked> objects;
        if (gameObj.player.playertype == PlayerType.Oponent)
        {
          objects = GameSceneManager.Instance.ownPlayer.GetObjects();
        }
        else
        {
          objects = GameSceneManager.Instance.oponentPlayer.GetObjects();
        }

        List<IAttacked> groundObjects = new List<IAttacked>();

        foreach(IAttacked obj in objects)
        {
            if(((Charater)obj).type == CharaterType.Ground)
            {
                groundObjects.Add(obj);

            }

        }

        objects = groundObjects;

        gameObj.attackTarget = (Charater)FindMinDistanceObj(objects);
    }
}
