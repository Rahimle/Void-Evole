using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // bien luu tru Prefab dan
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ham Input, nhan vat ban ra dan moi khi space
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Player is shooting!");
            // ham Instantiate de tao ra dan
            Instantiate(projectilePrefab,transform.position,Quaternion.identity);
        }
    }
}
