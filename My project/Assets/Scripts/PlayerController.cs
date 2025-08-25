using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // tốc độ di chuyển
    public GameObject projectilePrefab; // prefab viên đạn
    public Transform firePoint; // chỗ bắn đạn
    public float fireRate = 0.3f; // thời gian giữa các phát
    private float nextFire = 0f;

    void Update()
    {
        // --- Di chuyển ---
        float moveX = Input.GetAxisRaw("Horizontal"); // mũi tên trái/phải hoặc A/D
        float moveY = Input.GetAxisRaw("Vertical");   // mũi tên lên/xuống hoặc W/S
        Vector3 moveDir = new Vector3(moveX, moveY, 0).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // --- Bắn đạn ---
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        }
    }
}
