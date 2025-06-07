using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerID;
    public bool isShooter = false;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;

    public void SetRole(bool shooter)
    {
        isShooter = shooter;
        // Aquí puedes añadir efectos visuales al cambiar rol
    }

    public void Shoot()
    {
        if (!isShooter || bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null) bulletScript.shooterID = playerID;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = firePoint.forward * bulletSpeed;
    }
}