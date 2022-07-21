

public class DefenceBuff :Buff
{

    public override Buff GetCloneBuff()
    {


        DefenceBuff defenceBuff = new DefenceBuff();
        defenceBuff.value = this.value;
        defenceBuff.buffName = this.buffName;
        defenceBuff.buffType = this.buffType;
        defenceBuff.duration = this.duration;


        return defenceBuff;
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
