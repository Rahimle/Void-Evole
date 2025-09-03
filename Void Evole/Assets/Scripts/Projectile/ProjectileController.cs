using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f;              // toc do projectile
    public GameObject hitEffectPrefab;     // bien hieu ung khi hit enemy

    private GameObject target;             // muc tieu hien tai
    private Vector3 direction;             // huong ban

    public void SetTarget(GameObject enemy)
    {
        target = enemy;

        if (target != null)
        {
            // tinh huong tu player -> enemy
            direction = (target.transform.position - transform.position).normalized;

            // xoay projectile theo goc angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Start()
    {
        Destroy(gameObject,5f);// huy dan sau 5s neu ko va cham
    }

    void Update()
    {
        // projectile move theo huong
        transform.position += direction * speed * Time.deltaTime;
    }

    // ham va cham
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Find script EnemyController on enemy get hit
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();

            if (enemyController != null)// kiem tra ham ton tai ko
            {
                // goi ham TakeDamage on enemy, truyen damage la 1
                enemyController.TakeDamage(1);
            }
            
            // explosion hit effect
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 1f);
            }
            
            Destroy(gameObject);// xoa projectile
        }
    }

    // ham dc goi trc khi doi tuong bi destroy
    void OnDestroy()
    {
        // check if projectile destroyed by hit ?
        // if no, thong bao cho PlayerShooting
        if (FindObjectOfType<PlayerShooting>() != null)
        {
            FindObjectOfType<PlayerShooting>().OnProjectileDestroyed();
        }
    }
}
