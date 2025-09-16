using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // bien Singleton GameManager
    public static GameManager Instance { get; private set; }

    //bien quan ly trang thai game
    public int wallHealth = 20;
    public int enemiesActive { get; private set; } = 0;

    // bien tham chieu thanh phan UI
    public TextMeshProUGUI uiWallHealth;    

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
        UpdateUIWallHealth(); // ham cap nhat UI hp wall
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Ham cap nhat UI health wall
    private void UpdateUIWallHealth()
    {
        if(uiWallHealth != null) // ktra value
        {
            uiWallHealth.text = "Wall Health: " + wallHealth.ToString(); // cap nhat
        }
    }

    // ham xu ly wall take damage
    public void TakeWallDamage(int damage)
    {
        wallHealth -= damage; // hp - damage taken
        if (wallHealth < 0)
        {
            wallHealth = 0;
        }
        UpdateUIWallHealth(); // cap nhat ui khi hp giam

        // kiem tra hp wall
        if(wallHealth <= 0)
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
