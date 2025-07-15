using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("1 para jugador 1, 2 para jugador 2")]
    public int playerID = 1;

    public float moveSpeed = 7f;
    public float esquiveCooldown = 2f;
    public float rotacionEsquive = 30f;
    public float tiempoEsquive = 0.5f;

    private float tiempoUltimoEsquive = -Mathf.Infinity;
    private int moveDirection = 0;

    private float initialPositionX;
    private Quaternion rotacionInicial; // Guardamos la rotación inicial
    private Quaternion rotacionObjetivo;
    private bool isEsquivando = false; // Para controlar cuándo aplicar la rotación de esquive

    void Start()
    {
        initialPositionX = transform.position.x;

        // Configurar rotación inicial según el playerID
        if (playerID == 1)
        {
            rotacionInicial = Quaternion.Euler(0, 0, 0);
        }
        else if (playerID == 2)
        {
            rotacionInicial = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            Debug.LogWarning("PlayerMovement: playerID no válido. Debe ser 1 o 2.");
            rotacionInicial = Quaternion.identity;
        }

        transform.rotation = rotacionInicial;
        rotacionObjetivo = rotacionInicial;
    }

    void Update()
    {
        // Movimiento horizontal
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

        // Solo interpolar rotación si está esquivando
        if (isEsquivando || transform.rotation != rotacionObjetivo)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionObjetivo, 5f * Time.deltaTime);
        }
    }

    public void MoveLeft()
    {
        if (Time.time < tiempoUltimoEsquive + esquiveCooldown) return;

        moveDirection = -1;
        tiempoUltimoEsquive = Time.time;
        isEsquivando = true;

        float angulo = (playerID == 1) ? rotacionEsquive : -rotacionEsquive;
        rotacionObjetivo = rotacionInicial * Quaternion.Euler(0, 0, angulo);

        Invoke(nameof(VolverCentro), tiempoEsquive);
    }

    public void MoveRight()
    {
        if (Time.time < tiempoUltimoEsquive + esquiveCooldown) return;

        moveDirection = 1;
        tiempoUltimoEsquive = Time.time;
        isEsquivando = true;

        float angulo = (playerID == 1) ? -rotacionEsquive : rotacionEsquive;
        rotacionObjetivo = rotacionInicial * Quaternion.Euler(0, 0, angulo);

        Invoke(nameof(VolverCentro), tiempoEsquive);
    }

    private void VolverCentro()
    {
        moveDirection = 0;
        rotacionObjetivo = rotacionInicial;
        isEsquivando = false;
    }

    public void StopMoving()
    {
        moveDirection = 0;
        rotacionObjetivo = rotacionInicial;
        isEsquivando = false;
    }
}
