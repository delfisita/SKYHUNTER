using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int playerID;
    public int maxLives = 3;
    public AudioClip hitSound;

    private AudioSource audioSource;
    private int currentLives;
    private Vector3 spawnPosition;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentLives = maxLives;
        spawnPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null && bullet.shooterID != playerID)
            {
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