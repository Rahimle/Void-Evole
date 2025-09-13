using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public GameObject enemyPrefab;// bien luu Prefab enemy
    //public float spawnBetweenWaves = 5f;// bien khoang time giua cac waves
    //public int enemiesQuantity = 1;// bien so luong quai

    //private float spawnTimer = 0f;// bien dem time

    public int currentWave = 1; // bien luu wave
    public int baseEnemiesPerWave = 5; // so luong quai/wave
    public float timeBetweenWaves = 5f; // time giua waves
    public GameObject waveUIPrefab; // bien luu connect PrefabUi

    // connect UI
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI countdownText;

    // so luong quai 1 lan spawn
    private int totalEnemiesThisWave;

    // Start is called before the first frame update
    void Start()
    {
        // 1. khoi tao PrefabUi khi game start
        GameObject waveUIInstance = Instantiate(waveUIPrefab);

        // 2. tim cac text ben trong GameUi
        TextMeshProUGUI[] uiTexts = waveUIInstance.GetComponentsInChildren<TextMeshProUGUI>();
        if (uiTexts.Length >= 2)
        {
            waveText = uiTexts[0];
            countdownText = uiTexts[1];
        }
        // 3. bat dau corountine 
        StartCoroutine(SpawnWavesRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        //spawnTimer = spawnTimer + Time.deltaTime;
        //if (spawnTimer >= spawnBetweenWaves)
        //{
        //    SpawnEnemies();// goi ham spawn quai
        //    spawnTimer = 0f;// reset time = 0
        //}
    }

    private IEnumerator SpawnWavesRoutine()
    {
        while (true) // endless loop for waves
        {
            if (waveText != null)
            {
                waveText.text = "Wave: 0";
            }
            // b1: time countdown
            float countdown = timeBetweenWaves;
            while (countdown >= 0)
            {
                if (countdownText != null)
                {
                    countdownText.text = "Next Wave in: " + Mathf.Round(countdown);
                }
                yield return new WaitForSeconds(1f);
                countdown--;
            }

            // b2: start new wave
            totalEnemiesThisWave = baseEnemiesPerWave * currentWave;
            if (waveText != null)
            {
                waveText.text = "Wave: " + currentWave;
            }

            // b3: spawn enemy
            for (int i = 0; i < totalEnemiesThisWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f); // spawn 1 con / 0.5 giay
            }

            // wait till all enemy remain gone
            yield return new WaitUntil(() => GameManager.Instance.enemiesRemaining <= 0);

            // waves increase and loop 
            currentWave++;
        }
    }

    // ham spawn quai
    void SpawnEnemy()
    {
        // lay vi tri cua gameobject spawner
        Vector3 spawnerPosition = transform.position;
        // tinh toan vi tri random quanh cong
        // Random.Range(X,Y,Z)
        Vector3 randomOffset = new Vector3(Random.Range(-3f, 3f), Random.Range(-2.5f, 1.5f), 0);

        // bien 
        Vector3 spawnPosition = Vector3.zero;
        bool positionFound = false;
        int check = 0; // ban dau cho check = 0
        int maxCheck = 10;// so lan thu

        while (!positionFound && check < maxCheck) // neu tim duoc vi tri trong
        {
            // tinh toan vi tri moi
            randomOffset = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
            spawnPosition = spawnerPosition + randomOffset;

            // kiem tra vi tri co an toan ko
            if (!CheckPosition(spawnPosition, 0.75f))// khoang cach giua cac enemy
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

                // thong bao cho GameManager
                GameManager.Instance.AddEnemyToCount();
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
            if (distance < minDistance)// neu khoang cach qua gan
            {
                return true;
            }
        }
        return false;
    }
}