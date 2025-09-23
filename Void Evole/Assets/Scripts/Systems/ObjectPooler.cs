using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allow to show Pool in Inspector in Unity Editor
[System.Serializable] 
public class Pool
{
    public string tag; // Name Tag for each Objects (Projectile, Enemy,..)
    public GameObject prefab; // origin prefab
    public int size; // size pool
}

public class ObjectPooler : MonoBehaviour
{
    // List of pools 
    public List<Pool> pools;

    /*
    Dictionary to store Objects created
    Dictionary<TKey,TValue> : tu dien anh xa giua key va value
    Queue<GameObject> : moi key chua hang doi (queue) cac GameObject 
     */
    Dictionary<string, Queue<GameObject>> poolDictionary;

    // Setup Singleton
    public static ObjectPooler Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            // ObjectPooler khong bi huy khi chuyen Scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Use pools to creat each Pool
        poolDictionary = new Dictionary<string,Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectQueue.Enqueue(obj); // Add object vao hang doi
            }
            poolDictionary.Add(pool.tag, objectQueue);
        }

    }

    // Get Object
    public GameObject GetPooledObject(string tag)
    {
        // 1.Check if tag exist
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.Log("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        
        // 2.Get object from queue
        Queue<GameObject> objectQueue = poolDictionary[tag];
        if (objectQueue.Count == 0) // Check if pool have object to get
        {
            Debug.LogWarning("Pool " + tag + " is empty.");
            return null;
        }
        GameObject objectToSpawn = objectQueue.Dequeue();

        // 3.Active object and bring to use
        objectToSpawn.SetActive(true);

        // 4.Return object back to queue
        objectQueue.Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
