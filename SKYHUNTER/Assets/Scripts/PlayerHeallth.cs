using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración")]
    public int playerNumber = 1;

    [Header("Efectos")]
    public AudioClip hitSound;
    public GameObject hitEffect;
    public Material hitMaterial;
    public float flashDuration = 0.1f;

    private AudioSource audioSource;
    private Material originalMaterial;
    private Renderer playerRenderer;
    private Vector3 spawnPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerRenderer = GetComponentInChildren<Renderer>();
        originalMaterial = playerRenderer.material;
        spawnPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeHit();
            Destroy(other.gameObject);
            PlayHitEffects(other.transform.position);
        }
    }

    public void TakeHit()
    {
        GameManager.Instance.PlayerHit();
        FlashPlayer();
    }

    private void PlayHitEffects(Vector3 position)
    {
        if (hitEffect != null)
        {
            Instantiate(hitEffect, position, Quaternion.identity);
        }

        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void FlashPlayer()
    {
        if (playerRenderer != null && hitMaterial != null)
        {
            playerRenderer.material = hitMaterial;
            Invoke(nameof(ResetMaterial), flashDuration);
        }
    }

    private void ResetMaterial()
    {
        if (playerRenderer != null)
        {
            playerRenderer.material = originalMaterial;
        }
    }

    public void ResetPlayer()
    {
        transform.position = spawnPosition;
        gameObject.SetActive(true);

        if (TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (playerRenderer != null)
        {
            playerRenderer.material = originalMaterial;
        }
    }
}