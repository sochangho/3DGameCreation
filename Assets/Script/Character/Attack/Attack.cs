using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public Charater gameObj;
    public void init(Charater charater)
    {
        gameObj = charater;
    }
    abstract public void AttackTarget(IAttacked target);

}
