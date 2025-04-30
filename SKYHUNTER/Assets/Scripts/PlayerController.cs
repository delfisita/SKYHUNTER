using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isShooter = false;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public void SetRole(bool shooter)
    {
        isShooter = shooter;
    }

    public void Shoot()
    {
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
    }

}
}
