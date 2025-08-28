using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public GameObject portalPrefab;
    public Transform spawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Nhấn P để tạo portal
        {
            Instantiate(portalPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
