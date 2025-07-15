using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("1 para jugador 1, 2 para jugador 2")]
    public int playerID = 1;

    public float moveSpeed = 7f;
    public float esquiveCooldown = 2f;
    public float rotacionEsquive = 30f;
    public float tiempoEsquive = 0.5f;

    [Header("Invencibilidad")]
    public float invincibilityDuration = 2f;  // Duración base
    public bool isInvincible = false;
    private float invincibilityTimer = 0f;

    private float tiempoUltimoEsquive = -Mathf.Infinity;
    private int moveDirection = 0;

    private float initialPositionX;
    private float rotacionBaseY;
    private float rotacionZActual = 0f;
    private float rotacionZObjetivo = 0f;

    public PlayerController controller; // referencia asignada en inspector o en Start()

    private Renderer playerRenderer;

    void Start()
    {
        initialPositionX = transform.position.x;

        rotacionBaseY = (playerID == 2) ? 180f : 0f;

        // Setear rotación base Y sin tocar Z ni X
        transform.rotation = Quaternion.Euler(0f, rotacionBaseY, 0f);

        playerRenderer = GetComponent<Renderer>();

        if (controller == null)
        {
            controller = GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        // Invencibilidad timer
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0f)
            {
                DisableInvincibility();
            }
        }

        if (controller != null && controller.isShooter) return;

        // Movimiento lateral con límite
        if (moveDirection != 0)
        {
            Vector3 movement = new Vector3(moveDirection, 0, 0) * moveSpeed * Time.deltaTime;
            Vector3 nuevaPos = transform.position + movement;

            float minX = initialPositionX - 1.5f;
            float maxX = initialPositionX + 1.5f;
            nuevaPos.x = Mathf.Clamp(nuevaPos.x, minX, maxX);

            transform.position = nuevaPos;
        }
        else
        {
            float newX = Mathf.Lerp(transform.position.x, initialPositionX, 2f * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        // Rotación suave: manteniendo la base Y + la inclinación Z del esquive
        Quaternion baseRotation = Quaternion.Euler(0f, rotacionBaseY, 0f);
        Quaternion leanRotation = Quaternion.Euler(0f, 0f, rotacionZObjetivo);
        transform.rotation = Quaternion.Lerp(transform.rotation, baseRotation * leanRotation, 5f * Time.deltaTime);
    }

    public void MoveLeft()
    {
        if (controller != null && controller.isShooter) return;
        if (Time.time < tiempoUltimoEsquive + esquiveCooldown) return;

        moveDirection = -1;
        tiempoUltimoEsquive = Time.time;

        rotacionZObjetivo = (playerID == 1) ? rotacionEsquive : -rotacionEsquive;

        Invoke(nameof(VolverCentro), tiempoEsquive);
    }

    public void MoveRight()
    {
        if (controller != null && controller.isShooter) return;
        if (Time.time < tiempoUltimoEsquive + esquiveCooldown) return;

        moveDirection = 1;
        tiempoUltimoEsquive = Time.time;

        rotacionZObjetivo = (playerID == 1) ? -rotacionEsquive : rotacionEsquive;

        Invoke(nameof(VolverCentro), tiempoEsquive);
    }

    private void VolverCentro()
    {
        moveDirection = 0;
        rotacionZObjetivo = 0f;
    }

    public void StopMoving()
    {
        moveDirection = 0;
        rotacionZObjetivo = 0f;
    }

    // Invencibilidad
    public void ActivateInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;

        if (playerRenderer != null)
        {
            // Ejemplo: cambiar color para mostrar invencibilidad
            playerRenderer.material.color = Color.yellow;
        }
    }

    private void DisableInvincibility()
    {
        isInvincible = false;
        if (playerRenderer != null)
        {
            playerRenderer.material.color = Color.white;
        }
    }

    // Ejemplo de recibir daño, bloqueado si invencible
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        // Aquí la lógica para perder vida o morir
    }
}
