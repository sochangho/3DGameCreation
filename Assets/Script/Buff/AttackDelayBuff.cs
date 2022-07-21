
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

    public override void BuffStart(AimObject parameter)
    {
        if(parameter is Tower)
        {
            Tower  tower = parameter as Tower;

            tower.attackdelayTime -= value;

        }
    }


    public override void BuffEnd(AimObject parameter)
    {
        if (parameter is Tower)
        {
            Tower tower = parameter as Tower;

            tower.attackdelayTime += value;

        }
    }

}
