using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración")]
 

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
        // Resta una vida genérica
        GameManager.Instance.PlayerHit();
        FlashPlayer();
    }

    private void PlayHitEffects(Vector3 pos)
    {
        if (hitEffect != null)
            Instantiate(hitEffect, pos, Quaternion.identity);

        if (hitSound != null)
            audioSource.PlayOneShot(hitSound);
    }

    private void FlashPlayer()
    {
        if (hitMaterial == null) return;
        playerRenderer.material = hitMaterial;
        Invoke(nameof(ResetMaterial), flashDuration);
    }

    private void ResetMaterial()
    {
        playerRenderer.material = originalMaterial;
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

        playerRenderer.material = originalMaterial;
    }
}
