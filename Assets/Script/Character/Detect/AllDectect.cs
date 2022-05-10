using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDectect : Detect
{
    public AllDectect(Object obj)
    {
        this.gameObj = obj;
    }

    public override void OnDetect()
    {
        List<Charater> charaters;
        if(((Charater)gameObj).player.playertype == PlayerType.Oponent)
        {
            charaters = GameSceneManager.Instance.ownPlayer.GetCharaters();
        }
        else
        {
            charaters = GameSceneManager.Instance.oponentPlayer.GetCharaters();
        }
        aimFind = new AimFind();
        aimFind.aimobj = null;
        aimFind.distance = 0;
        for(int i = 0; i < charaters.Count; i++)
        {
            float distance = Vector3.Distance(((Charater)gameObj).transform.position, charaters[i].transform.position);
            if (aimFind.aimobj == null)
            {
                aimFind.aimobj = charaters[i];
                aimFind.distance = distance;
                continue;
            }
            

            if(aimFind.distance > distance)
            {
                aimFind.aimobj = charaters[i];
                aimFind.distance = distance;

            }



        }


        
        if(aimFind.aimobj != null)
        {
            ((Charater)gameObj).attackTarget = (Charater)(aimFind.aimobj);
        }
        else
        {
            ((Charater)gameObj).attackTarget = null;
        }  

    }




}
