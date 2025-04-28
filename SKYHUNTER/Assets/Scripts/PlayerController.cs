using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isShooter = false;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public void SetRole(bool shooter)
    {
        isShooter = shooter;
    }

    public void Shoot()
    {
        if (isShooter)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}
