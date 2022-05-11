using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : GameManager<GameSceneManager>
{
    public Player ownPlayer;
    public Player oponentPlayer;

    

    public void GameSceneInit(Player own , Player oponent)
    {
        ownPlayer = own;
        oponentPlayer = oponent;

        ownPlayer.playertype = PlayerType.Own;
        oponentPlayer.playertype = PlayerType.Oponent;

    }
}
