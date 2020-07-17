﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPoolItem {
        public GameObject prefab;

        public Vector2 vector;

    }

    public static ObjectPooler SharedInstance;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;

    public List<Vector2> pooledObjectsTransform;

    void Awake()
    {
        SharedInstance = this;
        pooledObjects = new List<GameObject>();
        pooledObjectsTransform = new List<Vector2>();

        foreach (ObjectPoolItem item in itemsToPool)
        {   
            GameObject goblin = (GameObject)Instantiate(item.prefab);
            goblin.SetActive(false);
            pooledObjects.Add(goblin);
            pooledObjectsTransform.Add(item.vector);

        }
    }
    
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
        if (!pooledObjects[i].activeInHierarchy)
        {
        return pooledObjects[i];
        }
        }
        return null;
    }
    
    public int PooledLength()
    {
        return pooledObjects.Count;
    }

    public List<Vector2> GetTransformObject()
    {
        return pooledObjectsTransform;
    }
}