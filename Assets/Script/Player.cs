using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerType playertype;

    private List<Charater> charaters = new List<Charater>();

    public void AddCharacter(Charater charater)
    {
        charaters.Add(charater);

    }

    public List<Charater> GetCharaters()
    {

        return charaters;
    }

}
