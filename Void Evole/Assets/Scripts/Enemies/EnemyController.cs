using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.5f;// bien luu toc do quai
    private Transform playerTransform; // bien luu vi tri nguoi choi

    // Start is called before the first frame update
    void Start()
    {
        // tim vi tri nguoi choi 
        GameObject playerObject = GameObject.Find("Player");

        // neu tim thay thi luu vi tri ]
        if(playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // kiem tra neu tim thay player
        if (playerTransform != null)
        {
            // tinh toan huong di chuyen tu vi tri enemy -> player
            // .normalized dam bao vector co lenght = 1, giup speed di chuyen on dinh
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // di chuyen enemy theo huong 
            transform.Translate(direction * speed * Time.deltaTime);
        }

        if (transform.position.x < -10f)// neu ra khoi man hinh trai 
        {
            Destroy(gameObject);// huy 
        }
    }
}
