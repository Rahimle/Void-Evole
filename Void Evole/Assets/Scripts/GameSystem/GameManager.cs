using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // bien Singleton GameManager
    public static GameManager Instance { get; private set; }

    //bien quan ly trang thai game
    public float maxBarrierHealth = 100f;
    public float currentBarrierHealth;
    public int enemiesActive { get; private set; } = 0;

    // bien tham chieu thanh phan UI
    public TextMeshProUGUI barrierHealthText;
    public Slider barrierHealthSlider;

    // Ham thiet lap Singletone
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
        if (barrierHealthSlider != null)
        {
            barrierHealthSlider.maxValue = maxBarrierHealth;
        }
        UpdateBarrierUI(); // ham cap nhat UI hp barrier
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Ham cap nhat UI health barrier
    private void UpdateBarrierUI()
    {
        if(barrierHealthSlider != null)
        {
            barrierHealthSlider.value = currentBarrierHealth;
        }

        if(barrierHealthText != null)
        {
            barrierHealthText.text = "HP: " + currentBarrierHealth;
        }
    }

    // ham xu ly wall take damage
    public void TakeWallDamage(float damage)
    {
        currentBarrierHealth -= damage; // hp - damage taken
        if (currentBarrierHealth < 0)
        {
            currentBarrierHealth = 0;
        }
        UpdateBarrierUI(); // cap nhat ui khi hp giam

        // kiem tra hp wall
        if(currentBarrierHealth <= 0)
        {
            GameOver(); // call ham thua
        }
        
    }

    // Cac ham Add, Remove, GameOver
    public void AddEnemy()
    {
        enemiesActive++; // spawn enemy
    }
    public void RemoveEnemy()
    {
        enemiesActive--; // remove enemy
    }
    void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0; // tam dung game
    }
}
