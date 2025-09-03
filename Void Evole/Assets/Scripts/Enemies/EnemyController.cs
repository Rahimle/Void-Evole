using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // thu vien ho tro TextMeshPro

public class EnemyController : MonoBehaviour
{
    public float speed = 0.1f;// bien luu toc do quai
    public int health = 10;// bien luu hp enemy
    // bien tao ra 1 truong de keo tha doi tuong TextMeshPro vao trong Inspec Unity
    public TextMeshProUGUI healthText;

    private Transform playerTransform; // bien luu vi tri nguoi choi

    // Start is called before the first frame update
    void Start()
    {
        // tim vi tri nguoi choi 
        GameObject playerObject = GameObject.Find("Player");

        // neu tim thay thi luu vi tri ]
        if(playerObject != null)
        {
            playerTransform = playerObject.transform;
        }

        if(healthText != null)
        {
            healthText.text = health.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // kiem tra neu tim thay player
        if (playerTransform != null)
        {
            // tinh toan huong di chuyen tu vi tri enemy -> player
            // .normalized dam bao vector co lenght = 1, giup speed di chuyen on dinh
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // di chuyen enemy theo huong 
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if (transform.position.x < -10f)// neu ra khoi man hinh trai 
        {
            Destroy(gameObject);// huy 
        }
    }

    // ham takedamage dc goi tu script projectile khi enemy hit projectile
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount; // hp enemy - damage cua projectile
        // cap nhat gia tri hp text
        if(healthText != null)
        {
            healthText.text = health.ToString();
        }

        if(health <= 0) // neu hp enemy <= 0
        {
            Destroy(gameObject); // huy enemy
        }
    }
}
