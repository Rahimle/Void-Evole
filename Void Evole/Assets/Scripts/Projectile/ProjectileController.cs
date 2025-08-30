using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f; // toc do di chuyen cua dan

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
         * transform.Translate la ham trong Unity, di chuyen object theo 1 vector va speed da cho
         * Vector2.up la 1 vector luon huong len tren
         * speed * Time.deltaTime = quan duong di duoc trong 1 khung hinh
         */
        // di chuyen dan theo huong co dinh da tinh
        transform.Translate(Vector2.up * speed *  Time.deltaTime);

        // neu dan qua pham vi qua xa thi tu huy
        if(transform.position.x > 10f || transform.position.y > 6f || transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    // ham va cham voi enemy
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // kiem tra doi tuong co la Enemy ko
        {
            Destroy(collision.gameObject);// huy doi tuong
            Destroy(gameObject);// huy vien dan
        }
    }
}
