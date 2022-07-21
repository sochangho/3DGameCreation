

public class RangeBuff : Buff
{
    public override Buff GetCloneBuff()
    {


        RangeBuff rangeBuff = new RangeBuff();
        rangeBuff.value = this.value;
        rangeBuff.buffName = this.buffName;
        rangeBuff.buffType = this.buffType;
        rangeBuff.duration = this.duration;


        return rangeBuff;
    }
    public override void BuffStart(AimObject parameter)
    {
        parameter.range += value;
    }
    public override void BuffEnd(AimObject parameter)
    {
        parameter.range -= value;
    }

}
