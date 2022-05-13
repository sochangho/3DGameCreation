using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public AimObject gameObj;
    [HideInInspector]
    public Detect detect;
    abstract public void init(AimObject charater);  
    abstract public void AttackTarget(AimObject target);

}
