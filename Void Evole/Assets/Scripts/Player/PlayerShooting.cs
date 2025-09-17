using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Cau hinh shooting
    public float fireRate = 0.5f;
    public float shootingRange = 10f;

    // Bien quan ly trang thai
    private float fireTimer = 0f;
    private bool isShooting = false;
    public Transform firepoint;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerShooting is attached to: " + gameObject.name + " at position: " + transform.position);
    }

    void Update()
    {
        // Space turn on/off to shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = !isShooting;
            Debug.Log("Shooting State changed to " + isShooting);
        }

        if (isShooting)
        {
            fireTimer += Time.deltaTime;

            // kiem tra thoi gian ban
            if (fireTimer >= fireRate)
            {
                GameObject targetEnemy = FindNearestEnemy();
                if(targetEnemy != null)
                {
                    Shoot(targetEnemy);
                }
                fireTimer = 0f; // reset time 
            }
        }
    }

    // Ham Shoot
    void Shoot(GameObject targetEnemy)
    {
        // Get projectile from Object Pooler
        GameObject projectileInstance = ObjectPooler.Instance.GetPooledProjectile();
        if(projectileInstance != null)
        {
            // Dat vi tri cua vien dan tai Firepoint
            projectileInstance.transform.position = transform.position;
            projectileInstance.transform.rotation = Quaternion.identity; // Do nghieng cua vien dan

            // truyen vi tri cho projectile
            ProjectileController projectileScript = projectileInstance.GetComponent<ProjectileController>();
            if (projectileScript != null)
            {
                projectileScript.SetTarget(targetEnemy); // nhan 1 target
            }

        }
    }

    // Ham xac dinh Enemy
    GameObject FindNearestEnemy()
    {
        // Xac dinh all enemy tren scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            if(enemy.transform.position.x > transform.position.x)
            {
                float distance = Vector2.Distance(firepoint.position, enemy.transform.position);

                // Only xet nhung enemy trong range shoot
                if (distance < minDistance && distance <= shootingRange)
                {
                    minDistance = distance;
                    nearest = enemy;
                }
            }
        }
        return nearest;
    }
}
