using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;// bien luu tru Prefab dan
    public float fireRate = 0.5f; // toc do ban 1 vien / 0.5 giay
    public float aimingTime = 1f; // toc do ngam

    private float fireTimer = 0f; // bien dem time sau khi vien dan ban ra
    private bool isShooting = false; // trang thai ban la false
    private GameObject targetEnemy; // bien luu muc tieu

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) // kiem tra input 
        {
            isShooting = !isShooting; // bat trang thai ban
            Debug.Log("Shooting State changed to " + isShooting);// thong bao trang thai ban trong console
        }

        if(isShooting)
        {
            fireTimer = fireTimer + Time.deltaTime; // so dem ++

            // kiem tra thoi gian ban 
            if (fireTimer >= aimingTime) // aimmingTime de kiem soat thoi gian ban
            {
                targetEnemy = FindNearestEnemy(); // ham tim muc tieu gan nhat

                if(targetEnemy != null)
                {
                    Shooting();
                }
                fireTimer = 0f;// reset bo dem time
            }
        }
    }

    void Shooting()
    {
        // vi tri xuat hien cua dan nam gan player 1 don vi
        Vector3 spawnPoint = transform.position + new Vector3(1f, 0, 0);

        GameObject projectileInstance = Instantiate(projectilePrefab, spawnPoint, Quaternion.Euler(1f, 0, 0));

        // truyen vi tri cho vien dan
        ProjectileController projectileScript = projectileInstance.GetComponent<ProjectileController>();
        if (projectileScript != null && targetEnemy != null)
        {
            // truyen toan bo GameObject cua muc tieu
            projectileScript.SetTarget(targetEnemy);
        }
    }

    // ham tim enemy 
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
