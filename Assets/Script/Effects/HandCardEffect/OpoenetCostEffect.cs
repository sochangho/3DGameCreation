using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpoenetCostEffect : HandCardEffect
{

    public int value;
    public override void CardCilckTrigger(Player player)
    {

        Player usedPlayer = null;

        if(player.playertype == PlayerType.Oponent)
        {
            usedPlayer = GameSceneManager.Instance.ownPlayer;
        }
        else
        {
            usedPlayer = GameSceneManager.Instance.oponentPlayer;

        }


        if (usedPlayer.MaxCost + value < 0)
        {

            usedPlayer.MaxCost = 0;
        }
        else
        {


            usedPlayer.MaxCost += value;
        }
    }


    public override bool CardSelection()
    {
       int maxCost =  GameSceneManager.Instance.ownPlayer.MaxCost;


        if(maxCost + value >= 0)
        {

            return true;
        }


        return false;

        
    }

}
