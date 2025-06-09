using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int playerID;
    public bool isShooter = false;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 15f;
    public Button shootButton;

    public void SetRole(bool shooter)
    {
        isShooter = shooter;
        // Aquí puedes añadir efectos visuales al cambiar rol
    }

    public void Shoot()
    {
        if (!isShooter || bulletPrefab == null || firePoint == null || !shootButton.interactable)
            return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null) bulletScript.shooterID = playerID;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = firePoint.forward * bulletSpeed;
        StartCoroutine(CooldownCOrutine(1f));
    }

    private IEnumerator CooldownCOrutine(float cooldownTime)
    {
        shootButton.interactable = false;
        yield return new WaitForSeconds(cooldownTime);
        shootButton.interactable = true;
    }

}