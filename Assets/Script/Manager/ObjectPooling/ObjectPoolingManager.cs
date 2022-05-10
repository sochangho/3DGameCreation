using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ObjectPooling
{

    public class ObjectPoolingManager : GameManager<ObjectPoolingManager>
    {
        public Dictionary<string, Stack<GameObject>> objectsDictionary = new Dictionary<string, Stack<GameObject>>();




        public void AddObjects(string key, GameObject gameobj, int count)
        {

            if (objectsDictionary.ContainsKey(key))
            {

                for (int i = 0; i < count; i++)
                {

                    GameObject clone = Instantiate(gameobj);
                    clone.SetActive(false);

                    if (clone.GetComponent<ObjectPool>() == null)
                    {

                        clone.AddComponent<ObjectPool>().key = key;

                    }


                    objectsDictionary[key].Push(clone);


                }
                return;
            }

            objectsDictionary.Add(key, new Stack<GameObject>());
            for (int i = 0; i < count; i++)
            {

                GameObject clone = Instantiate(gameobj);
                clone.SetActive(false);


                if (clone.GetComponent<ObjectPool>() == null)
                {

                    clone.AddComponent<ObjectPool>().key = key;

                }

                objectsDictionary[key].Push(clone);


            }


        }



        public GameObject ObjectUse(string key)
        {

            if (!objectsDictionary.ContainsKey(key))
            {

                return null;
            }


            GameObject useObject = objectsDictionary[key].Pop();

            if (objectsDictionary[key].Count < 2)
            {
                AddObjects(key, useObject, 2);
            }


            useObject.SetActive(true);
            return useObject;

        }

        public void ObjectReturn(string key, GameObject gameobj)
        {
            if (!objectsDictionary.ContainsKey(key))
            {


                AddObjects(key, gameobj, 10);
                return;
            }


            gameobj.SetActive(false);
            objectsDictionary[key].Push(gameobj);

        }


        public void ObjectPoolingReset()
        {

            foreach (var element in objectsDictionary)
            {

                ObjectPool[] objects = FindObjectsOfType<ObjectPool>();

                for (int i = 0; i < objects.Length; i++)
                {

                    if (element.Key == objects[i].key)
                    {

                        ObjectReturn(element.Key, objects[i].gameObject);

                    }

                }
            }


            foreach (var element in objectsDictionary)
            {
                while (element.Value.Count > 0)
                {
                    GameObject obj = element.Value.Pop();
                    Destroy(obj);

                }


            }


            objectsDictionary.Clear();

        }
    }
}