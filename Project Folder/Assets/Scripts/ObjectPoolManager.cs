using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : SingletonScript<ObjectPoolManager>
{
    [HideInInspector] public List<GameObject> pooledObjects;
    [SerializeField] private Transform parent;
    [SerializeField] private ObjectPoolItem[] itemsToPool;
    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        for(int i = 0; i < itemsToPool.Length; i++)
        {
            if(itemsToPool[i].objectToPool.tag == tag)
            {
                GameObject obj = (GameObject)Instantiate(itemsToPool[i].objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
                return obj;
            }
            
        }

        return null;
    }
    private void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem goToPool in itemsToPool)
        {
            for (int i = 0; i < goToPool.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(goToPool.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }
}


[Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
}