using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public AimObject gameObj;
    public void init(AimObject charater)
    {
        gameObj = charater;
    }
    abstract public void AttackTarget(AimObject target);

}
