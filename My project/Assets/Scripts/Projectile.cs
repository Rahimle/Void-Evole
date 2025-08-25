using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Hủy đạn nếu bay ra khỏi màn hình
        if (transform.position.y > Camera.main.orthographicSize + 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // hủy enemy
            Destroy(gameObject); // hủy đạn
        }
    }
}
