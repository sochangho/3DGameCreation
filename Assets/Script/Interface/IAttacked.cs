

public interface IAttacked 
{
    void Hit(float damage);
}

public interface IObjectInfo
{

    float GetDamage();
    float GetHp();
    
}