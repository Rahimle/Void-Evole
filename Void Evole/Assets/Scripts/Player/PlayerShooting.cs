using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;// bien luu tru Prefab dan
    public float fireRate = 0.2f; // toc do ban 1 vien / 0.2 giay
    private float fireTimer = 0f; // bien dem time sau khi vien dan ban ra
    private bool isShooting = false; // trang thai ban la false

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
            if(fireTimer >= fireRate)
            {
                Shooting();// goi ham ban
                fireTimer = 0f; // reset dem = 0
            }
        }
    }

    void Shooting()
    {
        // vi tri xuat hien cua dan nam tren player 1 don vi
        Vector3 spawnPoint = transform.position + new Vector3(0, 1f, 0);
        Instantiate(projectilePrefab,spawnPoint, Quaternion.identity);
    }
}
