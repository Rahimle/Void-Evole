using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // Bien projectile
    public GameObject projectilePrefab;
    public int projectilePoolSize = 5; 
    private List<GameObject> pooledProjectiles;

    // Bien enemy
    public GameObject enemyPrefab;
    public int enemyPoolSize = 5; 
    private List<GameObject> pooledEnemies;

    // Setup Singleton
    public static ObjectPooler Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            // ObjectPooler khong bi huy khi chuyen Scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Khoi tao Pool projectile
        pooledProjectiles = new List<GameObject>();
        for(int i = 0; i<projectilePoolSize;i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            pooledProjectiles.Add(projectile);
        }

        // Khoi tao Pool enemy
        pooledEnemies = new List<GameObject>();
        for(int i = 0; i<enemyPoolSize;i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            pooledEnemies.Add(enemy);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Ham lay Projectile
    public GameObject GetPooledProjectile()
    {
        foreach (GameObject projectile in pooledProjectiles) // duyet cac object trong pool
        {
            if (!projectile.activeInHierarchy) // ktra hoat dong
            {
                projectile.SetActive(true);// kich hoat va return
                return projectile;
            }
        }
        // If end loops without object -> creat
        GameObject newProjectile = Instantiate(projectilePrefab);
        pooledProjectiles.Add(newProjectile);

        newProjectile.SetActive(true); // kich hoat va return new 
        return newProjectile;
    }

    public GameObject GetPooledEnemy()
    {
        foreach (GameObject enemy in pooledEnemies)
        {
            if(!enemy.activeInHierarchy) 
            {
                enemy.SetActive(true);
                return enemy;
            }
        }
        GameObject newEnemy = Instantiate(enemyPrefab);
        pooledEnemies.Add(newEnemy);

        newEnemy.SetActive(true);
        return newEnemy;
    }
}
