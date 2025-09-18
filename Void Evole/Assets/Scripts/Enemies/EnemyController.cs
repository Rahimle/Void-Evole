using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;// thu vien ho tro TextMeshPro

public class EnemyController : MonoBehaviour
{
    // Cau hinh enemy
    public float speed = 0.5f;
    public int maxHealth = 10;
    public float damageAmount = 10f;
    public float expValue = 1.5f;

    // Tham chieu UI
    public TextMeshProUGUI enemyHealthText;

    // Bien quan ly trang thai
    private int currentHealth;

    // Ham lam viec voi Object Pooler
    void OnEnable()
    {
        // Resey enemy's hp & update UI
        currentHealth = maxHealth;
        if(enemyHealthText != null)
        {
            enemyHealthText.text = "HP: " + currentHealth.ToString();
        }

        // Thong bao GameManager Addenemy dc kich hoat
        if(GameManager.Instance != null)
        {
            GameManager.Instance.AddEnemy();
        }
    }

    void OnDisable()
    {
        // Thong bao GameManger Remove enemy dc kich hoat
        if(GameManager.Instance != null)
        {
            GameManager.Instance.RemoveEnemy();
        }

        // Gui exp ve ExpManager khi enemy destroyed
        ExpManager expManager = FindObjectOfType<ExpManager>();
        if(expManager != null )
        {
            expManager.GainExperience(expValue);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Di chuyen enemy sang trai
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    // Ham take damage from player's projectile
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(enemyHealthText != null)
        {
            enemyHealthText.text = "HP: " + currentHealth.ToString(); // update Ui
        }

        if(currentHealth <= 0) // neu hp enemy <= 0
        {
            // vo hieu hoa enemy, kich hoat OnDisable()
            gameObject.SetActive(false);
        }
    }

    // Ham va cham
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            GameManager.Instance.TakeWallDamage(damageAmount);   
            gameObject.SetActive(false); 
        }
    }
}
