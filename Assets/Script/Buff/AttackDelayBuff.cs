
public class AttackDelayBuff : Buff
{
    public override Buff GetCloneBuff() 
    {


        AttackDelayBuff attackDelayBuff = new AttackDelayBuff();
        attackDelayBuff.value = this.value;
        attackDelayBuff.buffName = this.buffName;
        attackDelayBuff.buffType = this.buffType;
        attackDelayBuff.duration = this.duration;


        return attackDelayBuff;
    }
}
