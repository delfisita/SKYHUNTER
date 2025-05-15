using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Botones UI Jugador 1")]
    public GameObject player1ShootButton;
    public GameObject player1LeftButton;
    public GameObject player1RightButton;

    [Header("Botones UI Jugador 2")]
    public GameObject player2ShootButton;
    public GameObject player2LeftButton;
    public GameObject player2RightButton;

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
        // Singleton básico
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Desactivar todos los botones al iniciar
        player1ShootButton.SetActive(false);
        player1LeftButton.SetActive(false);
        player1RightButton.SetActive(false);

        player2ShootButton.SetActive(false);
        player2LeftButton.SetActive(false);
        player2RightButton.SetActive(false);

        InitializeGame();
        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (timer > 0f)
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
            HandlePlayerDeath();
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
        foreach (var ph in FindObjectsOfType<PlayerHealth>())
            ph.ResetPlayer();
    }

    private void UpdateUI()
    {
        timerText.text = $"Tiempo: {Mathf.CeilToInt(timer)}";
        livesText.text = $"Vidas: {currentLives}";
        roundText.text = $"Ronda: {currentRound}/{maxRounds}";
    }

    private void UpdateButtonsVisibility()
    {
        // Jugador 1
        player1ShootButton.SetActive(player1.isShooter);
        player1LeftButton.SetActive(!player1.isShooter);
        player1RightButton.SetActive(!player1.isShooter);
        // Jugador 2
        player2ShootButton.SetActive(player2.isShooter);
        player2LeftButton.SetActive(!player2.isShooter);
        player2RightButton.SetActive(!player2.isShooter);
    }

    private void HandlePlayerDeath()
    {
        currentRound++;
        if (currentRound > maxRounds) EndGame();
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
        if (currentRound > maxRounds) EndGame();
        else
        {
            currentLives = maxLives;
            SwapRoles();
            timer = roundDuration;
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
        // Ronda 1: Player1 dispara, Player2 esquiva
        player1.SetRole(true);
        player2.SetRole(false);
        UpdateButtonsVisibility();
    }

    private void SwapRoles()
    {
        bool p1s = player1.isShooter;
        player1.SetRole(!p1s);
        player2.SetRole(p1s);
        UpdateButtonsVisibility();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
