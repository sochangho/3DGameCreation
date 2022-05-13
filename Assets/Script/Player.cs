using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerType playertype;

    public List<Charater> charaters ;

    public List<Transform> pos;

    private int id = 0;

    public struct AttackedBundle
    {
        public IAttacked obj;
        public int id;

    }

    private List<AttackedBundle> objects = new List<AttackedBundle>();

    private void Start()
    {
      for(int i = 0; i < charaters.Count; i++)
        {
            var go  = Instantiate(charaters[i]);
            go.transform.position = pos[i].position;

            AddCharacter(go);

        }
        
     
    }


    public void AddCharacter(IAttacked obj)
    {
       Charater charater = ((Charater)(obj));
       charater.player = this;
       charater.ID = id;
       AttackedBundle attackedBundle = new AttackedBundle();
       attackedBundle.obj = charater;
       attackedBundle.id = id;
       id++;
        
       objects.Add(attackedBundle);
    }

    public void RemoveCharacter(IAttacked obj)
    {
        Charater charater = ((Charater)(obj));
        AttackedBundle attackedBundle  = objects.Find(x => x.id == charater.ID);        
        objects.Remove(attackedBundle);        
        
    }

    public List<IAttacked> GetObjects()
    {
        List<IAttacked> attackeds = new List<IAttacked>();

        for(int i = 0; i < objects.Count; i++)
        {
            attackeds.Add(objects[i].obj);

        }

        return attackeds;
    }

    

}
