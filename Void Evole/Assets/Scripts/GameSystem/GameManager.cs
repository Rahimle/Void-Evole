using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;// bien luu the hien GameManager
    public int wallHealth = 20; // bien luu wall hp
    public TextMeshProUGUI wallHealthText; // bien luu tham chieu den text wall hp

    void Awake() // thiet lap Singleton
    {
        if(Instance == null)// ktra co ton tai GM nao ko
        {
            Instance = this;// neu chua, gan the hien hien tai vao bien

            DontDestroyOnLoad(gameObject);// ngan ko cho object bi huy khi chuyen Scene
        }
        else // neu co
        {
            Destroy(gameObject);// huy object hien tai tranh dupe
        }
    }

    // ham va cham quai
    public void TakeWallDamage(int damageAmount)
    {
        wallHealth -= damageAmount; // hp wall giam theo damage nhan vao

        // dam bao hp ko dc duoi 0
        if (wallHealth < 0)
        {
            wallHealth = 0;
        }

        // cap nhat gia tri hien thi wall hp
        if (wallHealthText != null)
        {
            wallHealthText.text = wallHealth.ToString();
        }

        // kiem tra hp wall
        if(wallHealth <= 0)
        {
            GameOver(); // goi ham thua
        }
        
    }

    // ham GameOver
    public void GameOver()
    {
        Debug.Log("GameOver!");// thong bao thua

        Time.timeScale = 0; // dung game

    }


    // Start is called before the first frame update
    void Start()
    {
        // dam bao so hp hien thi khop voi gia tri ban dau
        if (wallHealthText != null)
        {
            wallHealthText.text = wallHealth.ToString();
        }
        wallHealthText = GameObject.Find("WallHealthText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
