using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;              // Prefab đạn
    public Transform firePoint;                      // Điểm bắn (con của Player, đặt ở nòng súng)
    public float fireRate = 0.5f;                    // khoảng cách giữa 2 phát bắn
    public Vector2 shootDelayRange = new Vector2(0.1f, 0.15f); // delay random sau khi aim gần đúng
    public float aimingThreshold = 5f;               // sai số góc cho phép (độ)
    public float minRotateSpeed = 90f;               // tốc độ xoay chậm nhất (độ/giây)
    public float maxRotateSpeed = 360f;              // tốc độ xoay nhanh nhất (độ/giây)

    private float fireTimer = 0f;
    private bool isShooting = false;
    private GameObject targetEnemy;
    private Quaternion targetRotation;

    void Update()
    {
        // Bật/tắt bắn
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = !isShooting;
            Debug.Log("Shooting State: " + isShooting);
        }

        if (!isShooting) return;

        fireTimer += Time.deltaTime;

        // Luôn tìm enemy gần nhất mỗi frame
        GameObject nearest = FindNearestEnemy();
        if (nearest != null)
        {
            if (targetEnemy != nearest) targetEnemy = nearest;

            // Tính hướng cần xoay
            Vector3 dir = (targetEnemy.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, 0, angle);

            // Xoay động: lệch nhiều xoay nhanh, lệch ít xoay chậm
            float angleDiff = Quaternion.Angle(transform.rotation, targetRotation);
            float t = Mathf.Clamp01(angleDiff / 180f);
            float dynamicSpeed = Mathf.Lerp(maxRotateSpeed, minRotateSpeed, 1f - t); // lệch lớn -> gần maxRotateSpeed
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, dynamicSpeed * Time.deltaTime);

            // Khi đã xoay gần đúng và đủ fireRate -> bắn sau một delay ngẫu nhiên
            angleDiff = Quaternion.Angle(transform.rotation, targetRotation);
            if (angleDiff <= aimingThreshold && fireTimer >= fireRate)
            {
                float randomDelay = Random.Range(shootDelayRange.x, shootDelayRange.y);
                StartCoroutine(ShootAfterDelay(randomDelay));
                fireTimer = 0f;
            }
        }
    }

    IEnumerator ShootAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (targetEnemy != null) Shooting();
        // Không reset target để tiếp tục bám mục tiêu gần nhất ở frame sau
    }

    void Shooting()
    {
        // Nếu có firePoint thì spawn tại đó; nếu chưa gán thì fallback ra trước mặt player 1 đơn vị
        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position + transform.right * 1f;
        Quaternion spawnRot = firePoint != null ? firePoint.rotation : transform.rotation;

        GameObject projectileInstance = Instantiate(projectilePrefab, spawnPos, spawnRot);

        ProjectileController projectileScript = projectileInstance.GetComponent<ProjectileController>();
        if (projectileScript != null && targetEnemy != null)
        {
            projectileScript.SetTarget(targetEnemy);
        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }
}
