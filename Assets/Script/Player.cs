using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerType playertype;

    public List<Charater> charaters ;

    private List<IAttacked> objects = new List<IAttacked>();


    private void Start()
    {
      for(int i = 0; i < charaters.Count; i++)
        {

            objects.Add(charaters[i]);
        }
        

    }


    public void AddCharacter(IAttacked obj)
    {
       Charater charater = ((Charater)(obj));
       charater.player = this;
       objects.Add(charater);
    }

    public List<IAttacked> GetObjects()
    {

        return objects;
    }

    

}
