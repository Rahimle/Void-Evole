using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f;              // tốc độ đạn
    public GameObject hitEffectPrefab;     // hiệu ứng khi trúng mục tiêu

    private GameObject target;             // mục tiêu hiện tại
    private Vector3 direction;             // hướng bắn

    public void SetTarget(GameObject enemy)
    {
        target = enemy;

        if (target != null)
        {
            // tính hướng từ player -> enemy
            direction = (target.transform.position - transform.position).normalized;

            // xoay đầu đạn theo hướng này
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Update()
    {
        // đạn di chuyển theo hướng
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // tạo hiệu ứng nổ
            if (hitEffectPrefab != null)
            {
                GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 1f);
            }

            // xóa enemy + đạn
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
