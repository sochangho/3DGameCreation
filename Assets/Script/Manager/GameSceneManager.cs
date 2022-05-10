using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : GameManager<GameSceneManager>
{
    public Player ownPlayer;
    public Player oponentPlayer;

    

    public void GameSceneInit()
    {
        Player[] players = FindObjectsOfType<Player>();

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].playertype == PlayerType.Oponent)
            {
                oponentPlayer = players[i];
                
            }
            else
            {

                ownPlayer = players[i];
            }


        }

    }
}
