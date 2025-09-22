using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Cau hinh Projectile
    public float speed = 10f;
    public int damageAmount = 1;
    public float lifeTime = 5f;

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
        if (lifeTimer >= lifeTime)
        {
            gameObject.SetActive(false);
        }
        if (target != null && target.activeInHierarchy) // update projectile way if target appear
        {
            direction = (target.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        }
        else // if no enemy appear
        {
            gameObject.SetActive(false); // Vo hieu hoa vien dan
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    // Ham reset time object activated from pool
    void OnEnable()
    {
        lifeTimer = 0f;
        target = null;
    }

    // Ham va cham
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(damageAmount);
            }

            // explosion hit effect from Pooler
            GameObject hitEffect = ObjectPooler.Instance.GetPooledEffectHit();
            if (hitEffect != null)
            {
                hitEffect.transform.position = transform.position;
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
