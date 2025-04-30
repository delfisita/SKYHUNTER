using UnityEngine;
using UnityEngine.UI; 
public class GameManager : MonoBehaviour
{
    public float roundDuration = 60f;
    private float timer;
    private int currentRound = 1;
    private int maxRounds = 3;

    public PlayerController player1;
    public PlayerController player2;

    public Text timerText; 

    private void Start()
    {
        timer = roundDuration;
        AssignRoles();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(timer).ToString();

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
       
    }

    private void AssignRoles()
    {
        player1.SetRole(true);
        player2.SetRole(false);
    }

    private void SwapRoles()
    {
       
        bool player1IsShooter = player1.isShooter;
        player1.SetRole(!player1IsShooter);
        player2.SetRole(player1IsShooter);
    }
}
