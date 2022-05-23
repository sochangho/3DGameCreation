using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffController 
{
    private AimObject aimObject;

    private List<BuffState> buffStates = new List<BuffState>();


    public void BuffTarget(AimObject aimObject)
    {

        this.aimObject = aimObject;
    }

    public void AddBuff(Buff buff, Image img = null, List<Material> materials = null)
    {
       

        
        for(int i = 0; i < buffStates.Count; i++)
        {
            if(buffStates[i].buff.buffName == buff.buffName)
            {
             
                buffStates[i].duration = buff.duration; 
                return;

            }

        }


        buff.BuffStart(aimObject);

        BuffState buffState = new BuffState();
       
        buffState.buff = buff;
        buffState.duration = buff.duration;
        buffState.buff.buffType = buff.buffType;
        buffState.Image = img;
        buffState.materials = materials;
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
            buffStates[i].buff.Buffproceeding(aimObject);
            if (buffStates[i].duration < 0)
            {
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
    public Image Image;
    public List<Material> materials;
    public bool isDead = false;

}
