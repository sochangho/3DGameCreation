using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ObjectPooling
{
    public class ObjectPool : MonoBehaviour
    {

        public string key;


        public void GameObjDead()
        {

            ObjectPoolingManager.Instance.ObjectReturn(key, this.gameObject);
        }

    }
}