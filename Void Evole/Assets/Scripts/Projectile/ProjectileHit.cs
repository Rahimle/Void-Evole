using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifeTime = 0.5f; // chỉnh bằng đúng thời gian animation nổ

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
