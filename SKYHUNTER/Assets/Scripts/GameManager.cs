using UnityEngine;
using UnityEngine.UI; // Para mostrar el tiempo opcional

public class GameManager : MonoBehaviour
{
    public float roundDuration = 60f; // 1 minuto
    private float timer;
    private int currentRound = 1;
    private int maxRounds = 3;

    public PlayerController player1;
    public PlayerController player2;

    public Text timerText; // Opcional, para mostrar el tiempo en UI

    private void Start()
    {
        timer = roundDuration;
        AssignRoles();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timer).ToString(); // Mostrar tiempo

        if (timer <= 0)
        {
            EndRound();
        }
    }

    private void EndRound()
    {
        currentRound++;

        if (currentRound > maxRounds)
        {
            EndGame();
        }
        else
        {
            SwapRoles();
            timer = roundDuration;
        }
    }

    private void EndGame()
    {
        Debug.Log("Fin del juego!");
        // Podrías mostrar pantalla de resultados, etc.
    }

    private void AssignRoles()
    {
      
        player1.SetRole(true);
        player2.SetRole(false);
    }

    private void SwapRoles()
    {
        // Invertir roles
        bool player1IsShooter = player1.isShooter;
        player1.SetRole(!player1IsShooter);
        player2.SetRole(player1IsShooter);
    }
}
