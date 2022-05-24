using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffController : MonoBehaviour
{
    private AimObject aimObject;

    private List<BuffState> buffStates = new List<BuffState>();


    public void BuffTarget(AimObject aimObject)
    {

        this.aimObject = aimObject;
    }

    public void AddBuff(Buff buff, GameObject particle) 
    {

       
        if(aimObject == null)
        {
            return;
        }

        
        for(int i = 0; i < buffStates.Count; i++)
        {
            if(buffStates[i].buff.buffName == buff.buffName)
            {
             
                buffStates[i].duration = buff.duration; 
                return;

            }

        }


        buff.BuffStart(aimObject);

        var go  = Instantiate(particle);
        go.transform.position = new Vector3(aimObject.transform.position.x, aimObject.transform.position.y + 1f, aimObject.transform.position.z);          
        go.transform.rotation = Quaternion.Euler(90, 0, 0);
        go.transform.parent = aimObject.transform;
      

        BuffState buffState = new BuffState();
       
        buffState.buff = buff;
        buffState.duration = buff.duration;
        buffState.buff.buffType = buff.buffType;
        buffState.effect = go;
        buffStates.Add(buffState);
    }


    public List<Buff> GetBuffs()
    {
        List<Buff> buffs = new List<Buff>();

        foreach(BuffState buffState in buffStates)
        {
            if (!buffState.isDead)
            {
                
                buffs.Add(buffState.buff);
            }
                    
        }

        for(int i = buffStates.Count - 1; i >= 0; i--)
        {
           
          
            if (buffStates[i].isDead)
            {
                Destroy(buffStates[i].effect);
                buffStates.Remove(buffStates[i]);
            }

        }

        

        return buffs;

    }


    public void BuffTimer()
    {

        for(int i = 0; i < buffStates.Count; i++)
        {

            if(buffStates[i].buff.buffType == Buff.BuffType.Permanent)
            {

                continue;
            }

            buffStates[i].duration -= Time.deltaTime;
            buffStates[i].buff.Buffproceeding(aimObject);
            if (buffStates[i].duration < 0)
            {
                Debug.Log("End");
                buffStates[i].duration = 0;
                buffStates[i].isDead = true;
                buffStates[i].buff.BuffEnd(aimObject);
            }

        }
    }


}


public class BuffState 
{
    public Buff buff;
    public float duration;
    public GameObject effect;
    public bool isDead = false;

}
