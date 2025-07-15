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

    private bool isInvincible = false;
    private Renderer playerRenderer;
    private Color originalColor;
    public Color invincibleColor = Color.yellow; // Color cuando es invencible

    void Start()
    {
        playerRenderer = GetComponentInChildren<Renderer>();
        if (playerRenderer != null)
            originalColor = playerRenderer.material.color;
    }

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

    // Método para activar invencibilidad
    public void ActivateInvincibility(float duration)
    {
        if (!isInvincible)
            StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true;
        if (playerRenderer != null)
            playerRenderer.material.color = invincibleColor;

        // Aquí podés desactivar colisiones o ignorar daños en otros scripts según tu implementación de daño.

        yield return new WaitForSeconds(duration);

        if (playerRenderer != null)
            playerRenderer.material.color = originalColor;
        isInvincible = false;
    }

    // Método para verificar si el jugador está invencible (puede usar PlayerHealth)
    public bool IsInvincible()
    {
        return isInvincible;
    }
}
