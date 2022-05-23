
using UnityEngine;

public class Buff : MonoBehaviour 
{
    public string buffName;
    public float value;
    public float duration;
    public enum BuffType
    {
        None,
        Permanent
    }

    public BuffType buffType;


   public virtual void BuffStart(AimObject parameter) { }

   public virtual void Buffproceeding(AimObject parmeter) { }

   public virtual void BuffEnd(AimObject parameter) { }

}
