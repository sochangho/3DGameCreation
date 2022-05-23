using UnityEngine;

public interface IAttacked 
{
    void Hit(AimObject damage);

    PlayerType AttackedObjectType();

}

public interface IObjectInfo
{

    float GetDamage();
    float GetHp();
    
}