using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Wave Status
    public int baseEnemies = 1; 
    public float waveCoolDown = 5f; 

    // Tham chieu UI
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI countdownText;

    // Manage Status
    private int currentWave = 1;
    private int enemiesToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // Funct start Coroutine
        StartCoroutine(SpawnWavesRoutine());
    }

    // Rountine Waves
    private IEnumerator SpawnWavesRoutine()
    {
        // endless loop for waves
        while (true) 
        {
            // 1. Count down & update UI
             waveText.text = "Wave: " + currentWave;
            float countdown = waveCoolDown;
            while (countdown > 0)
            {
                countdownText.text = "Next Wave in: " + Mathf.Round(countdown); // UI
                yield return new WaitForSeconds(1f); // delay 1 giay
                countdown--;
            }
            countdownText.text = "GO!"; // thong bao start wave

            // 2. Spawn enemies
            enemiesToSpawn = baseEnemies * currentWave;
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f); // spawn 1 con / giay
            }

            // 3. Wait till end wave 
            yield return new WaitUntil(() => GameManager.Instance.enemiesActive <= 0);

            // 4. Loop
            currentWave++; // increase wave
        }
    }

    // Spawn
    void SpawnEnemy()
    {
        GameObject enemy = ObjectPooler.Instance.GetPooledObject("Enemy");
        if(enemy != null)
        {
            // cap nhat vi tri
            Vector3 randomPos = new Vector3(transform.position.x,transform.position.y + Random.Range(-3f,3f), 0);
            enemy.transform.position = randomPos;
            enemy.SetActive(true);

        }

    }
}