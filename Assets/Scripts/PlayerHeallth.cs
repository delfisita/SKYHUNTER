using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerID;
    public int maxLives = 3;
    public AudioClip hitSound;

    private AudioSource audioSource;
    private int currentLives;
    private Vector3 spawnPosition;
    private PlayerController playerController; // Agregado

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentLives = maxLives;
        spawnPosition = transform.position;

        // Obtener referencia al PlayerController
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogWarning("PlayerHealth: No se encontró PlayerController en el mismo GameObject.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null && bullet.shooterID != playerID)
            {
                // Ignorar daño si está invencible
                if (playerController != null && playerController.IsInvincible())
                {
                    // Opcional: destruir la bala pero no hacer daño
                    Destroy(other.gameObject);
                    return;
                }

                Destroy(other.gameObject);
                TakeHit();
            }
        }
    }

    public void TakeHit()
    {
        currentLives--;
        GameManager.Instance.PlayerHit(playerID);

        if (audioSource != null && hitSound != null)
            audioSource.PlayOneShot(hitSound);

        // Opcional: aquí podés agregar efectos de daño, animaciones, etc.
    }

    public void ResetPlayer()
    {
        currentLives = maxLives;
        transform.position = spawnPosition;
        gameObject.SetActive(true);

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
