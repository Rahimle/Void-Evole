using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Cau hinh Wave (chinh sua trong inspector)
    public int baseEnemies = 1; // quantity of enemy each wave
    public float waveCoolDown = 5f; // time dem nguoc between waves

    // bien tham chieu UI 
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI countdownText;

    // bien quan ly trang thai
    private int currentWave = 1;
    private int enemiesToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        // goi ham coroutine bat dau wave
        StartCoroutine(SpawnWavesRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

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

    // Ham spawn enemy
    void SpawnEnemy()
    {
        GameObject enemy = ObjectPooler.Instance.GetPooledEnemy();
        if(enemy != null)
        {
            // cap nhat vi tri
            Vector3 randomPos = new Vector3(transform.position.x,transform.position.y + Random.Range(-3f,3f), 0);
            enemy.transform.position = randomPos;
            enemy.SetActive(true);

        }

    }

    // Ham va cham
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                // them luc push enemy
                Vector2 pushDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(pushDirection * 2f, ForceMode2D.Impulse);
            }
        }
    }
}