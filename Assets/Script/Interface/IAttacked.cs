using UnityEngine;

public interface IAttacked 
{
    void Hit(AimObject damage);

}

public interface IObjectInfo
{

    float GetDamage();
    float GetHp();
    
}