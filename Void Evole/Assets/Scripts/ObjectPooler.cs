using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject projectilePrefab; // bien tham chieu prefab projectile
    public GameObject enemyPrefab; // bien tham chieu prefab enemy
    public int projectilePoolSize = 5; // kich thuoc pool ( quantity of projectile created)
    public int enemyPoolSize = 5; // kich thuoc pool (quantity of enemy created)

    private List<GameObject> pooledProjectiles; // danh sach luu cac doi tuong projectile
    private List<GameObject> pooledEnemies; // danh sach luu cac doi tuong enemy

    // setup Singleton
    public static ObjectPooler Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // khoi tao project qua loops va them vao pool
        pooledProjectiles = new List<GameObject>();
        for (int i = 0; i < projectilePoolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab); // tao ra ban sao Prefab dan
            projectile.SetActive(false); // set trang thai dan ko hoat dong, hiden
            pooledProjectiles.Add(projectile); // them projectile dc tao vao list
        }

        // khoi tao enemy 
        pooledEnemies = new List<GameObject>();
        for (int i = 0; i < enemyPoolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab); // tao ban sao Prefab enemy
            enemy.SetActive(false); // set status unused, hiden
            pooledEnemies.Add(enemy); // them enemy dc tao vao list
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ham dc goi khi can 1 vien dan
    public GameObject GetPooledProjectile()
    {
        foreach (GameObject projectile in pooledProjectiles) // kiem tra tung vien dan trong list
        {
            // kiem tra xem dan co dang dc su dung ko
            if (!projectile.activeInHierarchy)
            {
                projectile.SetActive(true);// kich hoat projectile xuat hien de su dung
                return projectile;
            }
        }
        //neu loops end ma ko tim dc dan ko su dung thi tao them dan
        GameObject newProjectile = Instantiate(projectilePrefab);
        pooledProjectiles.Add(newProjectile);
        return newProjectile;
    }

    public GameObject GetPooledEnemy()
    {
        foreach (GameObject enemy in pooledEnemies) // kiem tra tung con quai trong list
        {
            if(!enemy.activeInHierarchy)
            {
                enemy.SetActive(true); // kich hoat enemy xuat hien -> in use
                return enemy;
            }
        }
        // if loops end,ko tim dc enemy thi tao them
        GameObject newEnemy = Instantiate(enemyPrefab);
        pooledEnemies.Add(newEnemy);
        return newEnemy;
    }
}
