using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerManager : Singleton<ObjectPoolerManager>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools = new List<Pool>();

    Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();
    Transform poolsSpawnHolder;

    private void Start()
    {
        poolsSpawnHolder = new GameObject("PoolsSpawnHolder").transform;
        CreateObjects();
    }

    void CreateObjects()
    {
        foreach (Pool p in pools)
        {
            Transform spawnHolder = new GameObject(p.tag).transform;
            spawnHolder.SetParent(poolsSpawnHolder);

            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < p.size; i++)
            {
                GameObject newGo = Instantiate(p.prefab);
                newGo.SetActive(false);
                queue.Enqueue(newGo);
                newGo.transform.SetParent(spawnHolder);
            }

            poolDictionary.Add(p.tag, queue);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag: " + tag + " doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        if (objectToSpawn == null)
            Debug.LogError(tag + " Was destroyed!");

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IObjectPooler resetObject = objectToSpawn.GetComponent<IObjectPooler>();

        if (resetObject != null)
            resetObject.PrepareObjectToSpawn();
        else
            Debug.LogWarning("There is no IObjectPooler interface!");

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}