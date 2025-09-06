using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public GameObject enemyPrefab;// bien luu Prefab enemy
    public float spawnBetweenWaves = 5f;// bien khoang time giua cac waves
    public int enemiesQuantity = 5;// bien so luong quai
    private float spawnTimer = 0f;// bien dem time
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer = spawnTimer + Time.deltaTime;
        if(spawnTimer >= spawnBetweenWaves)
        {
            SpawnEnemies();// goi ham spawn quai
            spawnTimer = 0f;// reset time = 0
        }
    }

    // ham spawn quai
    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesQuantity; i++)
        {
            // lay vi tri cua gameobject spawner
            Vector3 spawnerPosition = transform.position;

            // tinh toan vi tri random quanh cong
            // Random.Range(X,Y,Z)
            Vector3 randomOffset = new Vector3(Random.Range(-3f, 3f),Random.Range(-3f, 3f),0);

            // bien 
            Vector3 spawnPosition = Vector3.zero;
            bool positionFound = false;
            int check = 0; // ban dau cho check = 0
            int maxCheck = 10;// so lan thu

            while (!positionFound && check < maxCheck) // neu tim duoc vi tri trong
            {
                // tinh toan vi tri moi
                randomOffset = new Vector3(Random.Range(-3f, 3f), Random.Range(-5f, 5f), 0);
                spawnPosition = spawnerPosition + randomOffset;

                // kiem tra vi tri co an toan ko
                if(!CheckPosition(spawnPosition,1.5f))// 1.5 la khoang cach giua cac enemy
                {
                    // neu an toan thi xac dinh vi tri la true
                    positionFound = true;
                }
                check++;// tang so lan check len 1
            }

            // neu tim dc vi tri an toan 
            if (positionFound)
            {
                //Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);// trieu hoi enemy tai vi tri cu the
                
                GameObject enemyInstance = ObjectPooler.Instance.GetPooledEnemy();// lay enemy tu pool

                // kiem tra enemy co ton tai ko
                if (enemyInstance != null)
                {
                    enemyInstance.transform.position = spawnPosition;
                    enemyInstance.transform.rotation = Quaternion.identity;
                }
            }
        } 
    }

    // ham kiem tra vi tri spawn cua quai
    bool CheckPosition(Vector3 position, float minDistance) // 2 tham so la vi tri, khoang cach
    {
        // xac dinh cac gameobject ten Enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // dung foreach loc enemy
        foreach (GameObject enemy in enemies)
        {
            // tinh khoang cach den vi tri spawn moi
            float distance = Vector3.Distance(position, enemy.transform.position);

            if(distance < minDistance)// neu khoang cach qua gan
            {
                return true;
            }
        }
        return false;
    }
}
