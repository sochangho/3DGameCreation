
public class DamageBuff : Buff
{
    public override Buff GetCloneBuff()
    {


        DamageBuff damageBuff = new DamageBuff();
        damageBuff.value = this.value;
        damageBuff.buffName = this.buffName;
        damageBuff.buffType = this.buffType;
        damageBuff.duration = this.duration;


        return damageBuff;
    }
}
