using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject enemyPrefab; // bien luu Prefab quai vat
    public Transform spawnGates; // bien luu vi tri gate
    public float spawnTimeMax = 2f; // bien luu khoang time spawn quai
    public float spawnTimeCount = 0f; // bien dem thoi gian
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimeCount = spawnTimeCount + Time.deltaTime;// dem thoi gian ++
        if(spawnTimeCount >= spawnTimeMax ) // neu dem den time max thi trieu hoi quai
        {
            SpawnEnemy();// goi ham trieu hoi quai
            spawnTimeCount = 0f;// set bien dem time = 0
        }
    }

    void SpawnEnemy()
    {
        // ham Instantiate tao ban sao enemyPrefab tai vi tri gate
        Instantiate(enemyPrefab,spawnGates.position,Quaternion.identity);
    }
}
