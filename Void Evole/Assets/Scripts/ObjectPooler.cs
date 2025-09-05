using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject projectilePrefab; // bien tham chieu prefab dan
    public int poolSize = 5; // kich thuoc pool ( quantity of projectile created)

    private List<GameObject> pooledProjectiles; // danh sach luu cac doi tuong projectile

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
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledProjectiles = new List<GameObject>();

        for(int i = 0; i< poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab); // tao ra ban sao Prefab dan
            projectile.SetActive(false); // set trang thai dan ko hoat dong, hiden
            pooledProjectiles.Add(projectile); // them dan dc tao vao list
        }
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
                projectile.SetActive(true);// kich hoat dan xuat hien de su dung
                return projectile;
            }
        }
        //neu loops end ma ko tim dc dan ko su dung thi tao them dan
        GameObject newProjectile = Instantiate(projectilePrefab);
        pooledProjectiles.Add(newProjectile);
        return newProjectile;
    }
}
