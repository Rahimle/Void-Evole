using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // bien Prefab dan
    public float fireRate = 0.5f;       // bien toc do ban 1 vien
    public float aimingTime = 1f;       // bien toc do ban

    private float fireTimer = 0f;       // bien dem time
    private bool isShooting = false;    // state shooting la false
    private GameObject targetEnemy;     // bien muc tieu

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // kiểm tra input 
        {
            isShooting = !isShooting; // bật trạng thái bắn
            Debug.Log("Shooting State changed to " + isShooting); // thông báo trạng thái bắn trong console
        }

        if (isShooting)
        {
            fireTimer = fireTimer + Time.deltaTime; // so dem ++

            // kiem tra thoi gian ban
            if (fireTimer >= aimingTime) // aimingTime kiem soat time ban
            {
                targetEnemy = FindNearestEnemy(); // ham tim muc tieu

                if (targetEnemy != null)
                {
                    Shooting();
                }
                fireTimer = 0f; // reset bo dem = 0
            }
        }
    }

    void Shooting()
    {
        // vi tri vien dan gan player 1 don vi 
        Vector3 spawnPoint = transform.position + new Vector3(1f, 0, 0);

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
