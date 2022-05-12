using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Detect : MonoBehaviour
{
    
    public AimObject gameObj;

    public struct AimFind
    {
        public Object aimobj ;
        public float distance ;
    }
     
    public void init(AimObject charater)
    {
        gameObj = charater;
    }

    virtual public void OnDetect(){}

    public Object FindMinDistanceObj(List<IAttacked> objs){
          
          AimFind aimFind = new AimFind();
          aimFind.aimobj = null;
          aimFind.distance = 0;
        
        for (int i = 0; i < objs.Count; i++){
            
            float distance = Vector3.Distance(gameObj.transform.position
       , ((AimObject)objs[i]).gameObject.transform.position);

            Debug.Log(distance);
            if(aimFind.aimobj == null || aimFind.distance > distance){
                  aimFind.aimobj = ((AimObject)objs[i]);
                  aimFind.distance = distance;
                
            }
        }

        return aimFind.aimobj;

    }


}
