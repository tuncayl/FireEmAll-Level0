using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Objectpool : MonoBehaviour
{
    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> Pooledobjects;
        public GameObject objectPrefab;
        public int poolSize;
    }
    [SerializeField] private Pool[] pools = null;
    private void Awake()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].Pooledobjects = new Queue<GameObject>();
            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject obj = Instantiate(pools[j].objectPrefab);
                obj.SetActive(false);

                pools[j].Pooledobjects.Enqueue(obj);
            }
        }
    }

    public GameObject PInstantiate(types type, Vector3 Position)
    {
        int number = (int)type;
        if (number >= pools.Length) { return null; }

        GameObject obj = pools[number].Pooledobjects.Dequeue();

        {
            obj.transform.position = Position;
            obj.SetActive(true);
        }

        pools[number].Pooledobjects.Enqueue(obj);
        return obj;
    }

}
