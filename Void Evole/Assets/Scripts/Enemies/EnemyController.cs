using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;// thu vien ho tro TextMeshPro

public class EnemyController : MonoBehaviour
{
    // Status enemy
    public float speed = 1f;
    public int maxHealth = 10;
    public float damageAmount = 10f;

    // Physics and Push
    public float pushForce = 2f;

    // Tham chieu UI
    public TextMeshProUGUI healthText;
    private Rigidbody2D rb;

    // Manage status
    private float currentHealth;

    // Ham Awake dc goi khi GameObject dc tao
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();// lay tham chieu Rigid 1 lan duy nhat
        if(rb == null )
        {
            Debug.LogError("Rigidbody2D is missing on " + gameObject.name);
        }

        // Tham chieu Health Text
        healthText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Work with Object Pooler
    void OnEnable()
    {
        // Resey enemy's hp & update UI
        currentHealth = maxHealth;
        UpdateHealthUI();

        // Anoucement to GameManager when Add
        if(GameManager.Instance != null)
        {
            GameManager.Instance.AddEnemy();
        }
    }
    void OnDisable()
    {
        // Anoucement to GameManager when Remove
        if(GameManager.Instance != null)
        {
            GameManager.Instance.RemoveEnemy();
        }
    }

    // Ham su dung van toc cua Rigidbody2D de di chuyen
    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.left * speed;
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
        else if (collision.gameObject.CompareTag("Enemy")) // Them logic va cham giua cac ke dich
        {
            if (rb != null)
            {
                // them luc push enemy
                Vector2 pushDirection = (transform.position - collision.transform.position).normalized;
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }

    // Ham take damage from player's projectile
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(healthText != null)
        {
            healthText.text = "HP: " + currentHealth.ToString(); // update Ui
        }

        if(currentHealth <= 0) // neu hp enemy <= 0
        {
            // gain exp when enemy die
            if(ExpManager.Instance != null)
            {
                ExpManager.Instance.GainExperience(5);
            }
            // vo hieu hoa enemy, kich hoat OnDisable()
            gameObject.SetActive(false);
        }
    }

    // Update Health Text UI
    private void UpdateHealthUI()
    {
        if(healthText != null)
        {
            healthText.text = "HP: " + currentHealth.ToString();
        }
    }
}
