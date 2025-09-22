using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerShooting : MonoBehaviour
{
    // Status shooting
    public float shootingRange = 10f;
    public Transform firepoint;
    private float fireTimer = 0f;

    // Tham chieu den PlayerInteraction
    public PlayerInteraction playerInteraction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInteraction.isShooting)
        {
            fireTimer += Time.deltaTime;
            if(fireTimer >= playerInteraction.attackSpeed)
            {
                // Tim ke thu va ban
                GameObject targetEnemy = FindNearestEnemy();
                if(targetEnemy != null)
                {
                    Shoot(targetEnemy);
                }
                // reset time
                fireTimer = 0f;
            }
        }
    }

    // Ham Shoot
    void Shoot(GameObject targetEnemy)
    {
        // Get projectile from Object Pooler
        GameObject projectileInstance = ObjectPooler.Instance.GetPooledObject("Projectile");
        if (projectileInstance != null)
        {
            // Xac dinh vi tri va huong ban
            Vector3 startPosition;

            // Neu co FirePoint, dung vi tri FirePoint/ Neu ko co dung vi tri Player
            if (firepoint != null)
            {
                startPosition = firepoint.position;
            }
            else
            {
                startPosition = transform.position;
            }

            // Set up vi tri va huong cho vien dan
            projectileInstance.transform.position = startPosition;
            projectileInstance.transform.rotation = Quaternion.identity; // Do nghieng cua vien dan

            // Truyen muc tieu cho script ProjectileController
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
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        // Vi tri Player
        Vector2 playerPosition = transform.position;

        // Duyet tung ke thu, tim nearest
        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.position.x <= playerPosition.x)
            {
                continue;
                
            }
            float distance = Vector2.Distance(playerPosition, enemy.transform.position);

            // Only xet nhung enemy trong range shoot
            if (distance < minDistance && distance < shootingRange)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
