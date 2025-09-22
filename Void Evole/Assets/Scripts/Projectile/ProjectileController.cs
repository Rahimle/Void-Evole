using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Status Projectile
    public float speed = 10f;
    public float lifeTime = 5f;

    // Tham chieu script
    public PlayerInteraction playerInteraction;

    // Bien quan ly trang thai
    private GameObject target;
    private Vector3 direction;
    private float lifeTimer;

    // Ham reset time object activated from pool
    void OnEnable()
    {
        lifeTimer = 0f;
        target = null;
    }

    // Ham di chuyen projectile & check time exist
    void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            gameObject.SetActive(false);
            return;
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    // Ham va cham
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.TakeDamage(playerInteraction.attackDamage);
            }

            // explosion hit effect from Pooler
            GameObject hitEffect = ObjectPooler.Instance.GetPooledObject("EffectHit");
            if (hitEffect != null)
            {
                hitEffect.transform.position = transform.position;
            }

            gameObject.SetActive(false);
        }
    }

    // Ham muc tieu
    public void SetTarget(GameObject enemyTarget)
    {
        target = enemyTarget;
        if (target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
