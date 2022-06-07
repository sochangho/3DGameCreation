using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostEffect : HandCardEffect
{


    public int value;
    public override void CardCilckTrigger(Player player)
    {
        if (player.MaxCost + value < 0)
        {

            player.MaxCost = 0;
        }
        else
        {


            player.MaxCost += value;
        }

        
        
    }

    public override bool CardSelection()
    {
        int maxCost = GameSceneManager.Instance.oponentPlayer.MaxCost;


        if (maxCost + value >= 0)
        {

            return true;
        }


        return false;
    }

}
