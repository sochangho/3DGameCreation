using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostEffect : HandCardEffect
{


    public int value;
    public override void CardCilckTrigger(Player player)
    {


        player.MaxCost += value;
        
    }
}
