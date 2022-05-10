using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController 
{
    private List<BuffState> buffStates = new List<BuffState>();

    public void AddBuff<T>(float value , float duration , Buff.BuffType buffType) where T  : Buff , new()
    {
        T buff = new T();

        
        for(int i = 0; i < buffStates.Count; i++)
        {
            if(buffStates[i].buff is T)
            {
                buff.value = value;
                buffStates[i].buff = buff;
                return;

            }

        }

        

        BuffState buffState = new BuffState();
        buff.value = value;
        buffState.buff = buff;
        buffState.duration = duration;
        buffState.buff.buffType = buffType;
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

            if (buffStates[i].duration < 0)
            {
                buffStates[i].duration = 0;
                buffStates[i].isDead = true;
            }

        }
    }


}


public class BuffState 
{
    public Buff buff;
    public float duration;
    public bool isDead = false;


 
}
