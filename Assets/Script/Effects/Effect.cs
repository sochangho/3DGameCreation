using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Effect : Card , ICardSelectCondition
{

   public List<Buff> buffs;

   virtual public bool CardSelection()
    {

        return true;
    }


}

 