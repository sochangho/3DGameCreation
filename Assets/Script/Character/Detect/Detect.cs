using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Detect : MonoBehaviour
{
    //Ÿ���� �ɼ����ְ� ĳ���Ͱ� �ɼ�������
    public Object gameObj;

    public struct AimFind
    {
        public Object aimobj ;
        public float distance ;
    }

    public AimFind aimFind;


    abstract public void OnDetect();
}
