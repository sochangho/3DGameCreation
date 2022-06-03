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

        usedPlayer.MaxCost += value; 

    }
}
