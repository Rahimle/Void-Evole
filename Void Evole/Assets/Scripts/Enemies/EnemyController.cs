using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;// bien luu toc do quai

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // di chuyen quai sang trai
        // Vector2.left laf vector chi huong sang trai (-1,0)
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        
        if(transform.position.x < -10f)// neu ra khoi man hinh trai 
        {
            Destroy(gameObject);// huy 
        }
    }
}
