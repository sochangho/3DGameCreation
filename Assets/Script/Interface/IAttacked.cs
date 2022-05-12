using UnityEngine;

public interface IAttacked 
{
    void Hit(Charater damage);

}

public interface IObjectInfo
{

    float GetDamage();
    float GetHp();
    
}