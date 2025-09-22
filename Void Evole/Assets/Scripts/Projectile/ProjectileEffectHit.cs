using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffectHit : MonoBehaviour
{
    // Time alive of effect 
    public float lifeTime = 0.5f;

    private void OnEnable()
    {
        // Active Coroutine to auto deactive effect
        StartCoroutine(DeactivateAfterTime());
    }

    // Coroutine 
    private IEnumerator DeactivateAfterTime()
    {
        // Wait for timelife
        yield return new WaitForSeconds(lifeTime);

        // deactive and return pool
        gameObject.SetActive(false);
    }
}
