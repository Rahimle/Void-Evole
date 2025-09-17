using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Cau hinh Projectile
    public float speed = 10f;
    public int damageAmount = 1;

    // Bien tham chieu Prefab
    public GameObject hitEffectPrefab;// bien hieu ung khi hit enemy

    // Bien quan ly trang thai
    private GameObject target;
    private Vector3 direction;
    private float lifeTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Ham di chuyen projectile & check time exist
    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= 5f)
        {
            gameObject.SetActive(false);
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    // Ham reset time between shooting
    void OnEnable()
    {
        lifeTimer = 0f;
    }

    // Ham va cham
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(damageAmount);
            }

            // explosion hit effect from Prefab
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 1f);
            }

            gameObject.SetActive(false);
        }
    }

    public void SetTarget(GameObject enemyTarget)
    {
        target = enemyTarget;
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            this.direction = direction;
        }
    }
}
