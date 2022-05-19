using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerType playertype;

    public Tower tower;
    public Transform towerPos;
    //일단 테스트 용도 
    public List<CharactorData> characterdatas;

    [HideInInspector]
    public int MaxCost = 10;
    [HideInInspector]
    public int curCost ;

    
    public float maxFill = 1;
   
    public float curFill = 0;
    [HideInInspector]
    public float fillRatio = 0.0001f;

    private int id = 0;

    public class ObjectBundle
    {
        public object obj;
        public int id;

    }


    //전체 카드 
    public Dictionary<int, CharactorData> totalCardDatas = new Dictionary<int, CharactorData>();

    //손위에 카드들 
    readonly public List<ObjectBundle> handCardDatas = new List<ObjectBundle>();

    public ObjectBundle nextCardObj = new ObjectBundle();

    //필드위에 올라온카드 
    private List<ObjectBundle> objects = new List<ObjectBundle>();

    public void Start()
    {
        Tower towerClone = Instantiate(tower);
        towerClone.transform.position = towerPos.position;
        
        AddCharacter(towerClone);
    }

    public void AddCharacter(IAttacked obj)
    {
        
       AimObject aimeObject = ((AimObject)(obj));
       aimeObject.player = this;
       aimeObject.ID = id;
       ObjectBundle attackedBundle = new ObjectBundle();
       attackedBundle.obj = aimeObject;
       attackedBundle.id = id;
       id++;
        
       objects.Add(attackedBundle);
    }

    public void RemoveCharacter(IAttacked obj)
    {
        AimObject charater = ((AimObject)(obj));
        ObjectBundle attackedBundle  = objects.Find(x => x.id == charater.ID);        
        objects.Remove(attackedBundle);        
        
    }

    public List<T> GetObjects<T>() where T : IAttacked
    {
        List<T> attackeds = new List<T>();

        for(int i = 0; i < objects.Count; i++)
        {
            attackeds.Add((T)objects[i].obj);

        }

        return attackeds;
    }

    public bool FillGage()
    {
       
        if (curFill >= maxFill)
        {
          
            curFill = maxFill;
            
            if (curCost <= MaxCost)
            {
                RefillCost();
                curFill = 0;
                return true;
                
            }
            return false;
        }

       
        curFill += fillRatio;

        return false;
    }

    public void RefillCost()
    {

        curCost = MaxCost;

    }


    public bool PurchaseCardCost(int cost)
    {

        if(cost > curCost)
        {

            return false;
        }

        curCost -= cost;

        return true;

    }
}
