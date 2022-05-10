using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Detect : MonoBehaviour
{
    //타워가 될수도있고 캐릭터가 될수도있음
    public Object gameObj;

    public struct AimFind
    {
        public Object aimobj ;
        public float distance ;
    }

    public AimFind aimFind;


    abstract public void OnDetect();
}
