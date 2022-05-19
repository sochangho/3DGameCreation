using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Detect 
{
    
    public AimObject gameObj;
    
    public struct AimFind
    {
        public Object aimobj ;
        public float distance ;
    }
     
  
    virtual public void OnDetect(){}

    public Object FindMinDistanceObj(List<IAttacked> objs){
          
          AimFind aimFind = new AimFind();
          aimFind.aimobj = null;
          aimFind.distance = 0;
        
        for (int i = 0; i < objs.Count; i++){
            
            float distance = Vector3.Distance(gameObj.transform.position
       , ((AimObject)objs[i]).gameObject.transform.position);

           
            if(aimFind.aimobj == null || aimFind.distance > distance){
                  aimFind.aimobj = ((AimObject)objs[i]);
                  aimFind.distance = distance;
                
            }
        }

        return aimFind.aimobj;

    }


}
