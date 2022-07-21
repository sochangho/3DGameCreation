

public class SeepdBuff : Buff
{
    public override Buff GetCloneBuff()
    {


        SeepdBuff speedBuff = new SeepdBuff();
        speedBuff.value = this.value;
        speedBuff.buffName = this.buffName;
        speedBuff.buffType = this.buffType;
        speedBuff.duration = this.duration;


        return speedBuff;
    }
    public override void BuffStart(AimObject parameter)
    {
        parameter.defence += value;
    }

    public override void BuffEnd(AimObject parameter)
    {
        parameter.defence -= value;
    }

}
