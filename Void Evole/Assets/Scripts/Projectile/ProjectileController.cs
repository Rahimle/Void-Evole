using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f; // toc do di chuyen cua dan

    private Vector3 targetPosition;// bien luu vi tri muc tieu
    private Transform targetTransform; // bien luu transform cua muc tieu
    
    private Vector2 initialDirection;// bien luu huong bay cua projectile
    private bool hasTarget = false;// bien luu state da co muc tieu chua

    public void SetTarget(GameObject targetObject)// ham nhan vi tri va states muc tieu tu playershooting
    {
        // kiem tra doi tuong la null trc khi gan
        if (targetObject != null)
        {
            //targetTransform = targetObject.transform;
            //targetPosition = targetObject.transform.position;
            // tinh toan huong bay 1 lan duy nhat
            initialDirection = (targetObject.transform.position - transform.position).normalized;
            hasTarget = true;
        }
        else // neu ko co muc tieu, dan tiep tuc bay
        {
            //targetTransform = null;
            //targetPosition = Vector3.zero;
            initialDirection = Vector2.up;
            hasTarget = !hasTarget;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // dan se tu huy neu ko va cham 
        Destroy(gameObject,5f);
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
        transform.Translate(initialDirection * speed *  Time.deltaTime);

        //if(targetTransform != null)// kiem tra muc tieu con ton tai ko
        //{
        //    // cap nhat vi tri muc tieu neu dang di chuyen
        //    targetPosition = targetTransform.position;

        //    // tinh toan huong di chuyen
        //    Vector3 direction = (targetPosition - transform.position).normalized;
        //    transform.Translate(direction * speed * Time.deltaTime);
        //}
        //else // neu ko co muc tieu, dan di chuyen thang
        //{
        //    transform.Translate(Vector2.right * speed * Time.deltaTime);
        //}

        // neu dan qua pham vi qua xa thi tu huy
        //if (transform.position.x > 10f)
        //{
        //    Destroy(gameObject);
        //}
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
