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

    }

    // Update is called once per frame
    void Update()
    {
        /*
         * transform.Translate la ham trong Unity, di chuyen object theo 1 vector va speed da cho
         * Vector2.up la 1 vector luon huong len tren
         * speed * Time.deltaTime = quan duong di duoc trong 1 khung hinh
         */
        transform.Translate(Vector2.up * speed *  Time.deltaTime);
        // neu dan qua pham vi 10 don vi thi tu huy
        if(transform.position.y > 10f)
        {
            Destroy(gameObject);
        }
    }
}
