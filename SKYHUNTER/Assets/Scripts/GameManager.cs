using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Configuración de Rondas")]
    public float roundDuration = 60f;
    public int maxRounds = 3;

    [Header("Configuración de Jugadores")]
    public int maxLives = 3;
    public PlayerController player1;
    public PlayerController player2;

    [Header("Interfaz de Usuario")]
    public Text timerText;
    public Text livesText;
    public Text roundText;
    public GameObject gameOverPanel;
    public Button restartButton;

    private float timer;
    private int currentRound = 1;
    private int currentLives;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGame();
        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            EndRound();
        }
    }

    public void PlayerHit()
    {
        currentLives--;
        UpdateUI();

        if (currentLives <= 0)
        {
            HandlePlayerDeath();
        }
    }

    private void InitializeGame()
    {
        timer = roundDuration;
        currentLives = maxLives;
        currentRound = 1;
        Time.timeScale = 1f;

        gameOverPanel.SetActive(false);
        UpdateUI();
        AssignRoles();

        ResetAllPlayers();
    }

    private void ResetAllPlayers()
    {
        PlayerHealth[] players = FindObjectsOfType<PlayerHealth>();
        foreach (var player in players)
        {
            player.ResetPlayer();
        }
    }

    private void UpdateUI()
    {
        timerText.text = $"Tiempo: {Mathf.CeilToInt(timer)}";
        livesText.text = $"Vidas: {currentLives}";
        roundText.text = $"Ronda: {currentRound}/{maxRounds}";
    }

    private void HandlePlayerDeath()
    {
        currentRound++;

        if (currentRound > maxRounds)
        {
            EndGame();
        }
        else
        {
            currentLives = maxLives;
            SwapRoles();
            timer = roundDuration;
            ResetAllPlayers();
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
            currentLives = maxLives;
            ResetAllPlayers();
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}