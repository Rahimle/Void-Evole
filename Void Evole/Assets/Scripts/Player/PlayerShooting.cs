using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // biến lưu trữ Prefab đạn
    public float fireRate = 0.5f;       // tốc độ bắn 1 viên / 0.5 giây
    public float aimingTime = 1f;       // tốc độ ngắm

    private float fireTimer = 0f;       // biến đếm time sau khi viên đạn bắn ra
    private bool isShooting = false;    // trạng thái bắn là false
    private GameObject targetEnemy;     // biến lưu mục tiêu

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // kiểm tra input 
        {
            isShooting = !isShooting; // bật trạng thái bắn
            Debug.Log("Shooting State changed to " + isShooting); // thông báo trạng thái bắn trong console
        }

        if (isShooting)
        {
            fireTimer = fireTimer + Time.deltaTime; // số đếm ++

            // kiểm tra thời gian bắn 
            if (fireTimer >= aimingTime) // aimingTime để kiểm soát thời gian bắn
            {
                targetEnemy = FindNearestEnemy(); // hàm tìm mục tiêu gần nhất

                if (targetEnemy != null)
                {
                    Shooting();
                }
                fireTimer = 0f; // reset bộ đếm time
            }
        }
    }

    void Shooting()
    {
        // vị trí xuất hiện của đạn nằm gần player 1 đơn vị
        Vector3 spawnPoint = transform.position + new Vector3(1f, 0, 0);

        // tạo viên đạn
        GameObject projectileInstance = Instantiate(projectilePrefab, spawnPoint, Quaternion.Euler(1f, 0, 0));

        // truyền vị trí cho viên đạn
        ProjectileController projectileScript = projectileInstance.GetComponent<ProjectileController>();
        if (projectileScript != null && targetEnemy != null)
        {
            // truyền toàn bộ GameObject của mục tiêu
            projectileScript.SetTarget(targetEnemy);
        }
    }

    // hàm tìm enemy 
    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        if (enemies.Length > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = enemy;
                }
            }
        }
        return nearest;
    }
}
