using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static PoolManager Instance;

    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;
    private Dictionary<string, Transform> poolParents;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();
        poolParents = new Dictionary<string, Transform>();

        foreach (Pool pool in pools)
        {
            List<GameObject> objectPool = new List<GameObject>();
            GameObject parentObject = new GameObject(pool.tag + " Pool");
            parentObject.transform.SetParent(this.transform);
            poolParents[pool.tag] = parentObject.transform;

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(parentObject.transform);
                objectPool.Add(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = null;

       
        foreach (GameObject obj in poolDictionary[tag])
        {
            if (!obj.activeInHierarchy)
            {
                objectToSpawn = obj;
                break;
            }
        }

       
        if (objectToSpawn == null)
        {
            objectToSpawn = ExpandPool(tag);
        }

        if (objectToSpawn != null)
        {
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
        }

        return objectToSpawn;
    }

    private GameObject ExpandPool(string tag)
    {
        if (!poolParents.ContainsKey(tag)) return null;

        foreach (Pool pool in pools)
        {
            if (pool.tag == tag)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(poolParents[tag]);
                poolDictionary[tag].Add(obj);
                return obj;
            }
        }

        return null;
    }
}
