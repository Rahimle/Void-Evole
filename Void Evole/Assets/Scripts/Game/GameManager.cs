using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton Game
    public static GameManager Instance { get; private set; }

    // Status Game
    public float maxBarrierHealth = 100f;
    public float currentBarrierHealth;
    public int enemiesActive { get; private set; } = 0;

    // Tham chieu UI
    public TextMeshProUGUI barrierHealthText;
    public Slider barrierHealthSlider;

    // Awake Singletone
    void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
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
        // Set up hp & slider at first
        currentBarrierHealth = maxBarrierHealth;
        UpdateBarrierUI(); // Call func update UI
    }

    // Take Damage
    public void TakeWallDamage(float damage)
    {
        currentBarrierHealth -= damage; // Hp decrease
        currentBarrierHealth = Mathf.Max(currentBarrierHealth,0); // HP can't not negative
        UpdateBarrierUI(); // Update UI

        // Check HP Barrier
        if(currentBarrierHealth <= 0)
        {
            GameOver(); // Call func if lose
        }
        
    }
    // Add & Remove
    public void AddEnemy()
    {
        enemiesActive++; // spawn enemy
    }
    public void RemoveEnemy()
    {
        enemiesActive--; // remove enemy
    }
    
    // Update Ui Barrier
    private void UpdateBarrierUI()
    {
        if (barrierHealthSlider != null)
        {
            barrierHealthSlider.maxValue = maxBarrierHealth;
            barrierHealthSlider.value = currentBarrierHealth;
        }

        if (barrierHealthText != null)
        {
            barrierHealthText.text = currentBarrierHealth + " / " + maxBarrierHealth;
        }
    }
    // GameOver
    private void GameOver()
    {
        Debug.Log("Game Over! Loading GameOver Scene...");
        SceneManager.LoadScene("GameOverScene");
    }
}
