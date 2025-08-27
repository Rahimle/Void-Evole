using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // toc do di chuyen cua dan
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // lay Rigidbody 2D va gan van toc cho no
        // transform.up la 1 vector chi huong len tren (0,1)
        GetComponent<Rigidbody2D>().velocity = transform.up * speed;

        // ham huy doi tuong sau khoang thoi gian
        Destroy(gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
