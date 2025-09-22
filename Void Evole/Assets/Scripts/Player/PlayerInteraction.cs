using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Status player
    public float attackDamage = 2f;
    public float attackSpeed = 0.5f;

    // Manager shooting status
    public bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Started!");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isShooting = true;
            Debug.Log("Shooting status is changed to " + isShooting);
        }
    }
}
