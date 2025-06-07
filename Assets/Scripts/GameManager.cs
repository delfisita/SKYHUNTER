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
    public PlayerHealth player1Health;
    public PlayerHealth player2Health;

    [Header("Interfaz de Usuario")]
    public Text timerText;
    public Text player1LivesText;
    public Text player2LivesText;
    public Text roundText;
    public GameObject gameOverPanel;
    public Button restartButton;

    private float timer;
    private int currentRound = 1;
    private bool gameEnded = false;
    private int player1Lives;
    private int player2Lives;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Inicialización de botones
        player1ShootButton.SetActive(false);
        player1LeftButton.SetActive(false);
        player1RightButton.SetActive(false);
        player2ShootButton.SetActive(false);
        player2LeftButton.SetActive(false);
        player2RightButton.SetActive(false);

        // Inicialización de vidas
        player1Lives = maxLives;
        player2Lives = maxLives;

        InitializeGame();
        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (!gameEnded && timer > 0f)
        {
            timer -= Time.deltaTime;
            UpdateUI();
        }
        else if (!gameEnded)
        {
            EndRound();
        }
    }

    public void PlayerHit(int playerID)
    {
        // Actualizar vidas
        if (playerID == 1) player1Lives--;
        else if (playerID == 2) player2Lives--;

        UpdateUI();

        // Verificar fin de ronda
        if (player1Lives <= 0 || player2Lives <= 0)
        {
            HandlePlayerDeath();
        }
    }

    private void InitializeGame()
    {
        timer = roundDuration;
        gameEnded = false;
        Time.timeScale = 1f;

        gameOverPanel.SetActive(false);
        UpdateUI();
        AssignRoles();
        ResetAllPlayers();
    }

    private void ResetAllPlayers()
    {
        player1Health.ResetPlayer();
        player2Health.ResetPlayer();
    }

    private void UpdateUI()
    {
        player1LivesText.text = $"J1: {player1Lives}/{maxLives}";
        player2LivesText.text = $"J2: {player2Lives}/{maxLives}";
        timerText.text = $"Tiempo: {Mathf.CeilToInt(timer)}";
        roundText.text = $"Ronda: {currentRound}/{maxRounds}";
    }

    private void UpdateButtonsVisibility()
    {
        player1ShootButton.SetActive(player1.isShooter);
        player1LeftButton.SetActive(!player1.isShooter);
        player1RightButton.SetActive(!player1.isShooter);

        player2ShootButton.SetActive(player2.isShooter);
        player2LeftButton.SetActive(!player2.isShooter);
        player2RightButton.SetActive(!player2.isShooter);
    }

    private void HandlePlayerDeath()
    {
        gameEnded = true;
        Time.timeScale = 0.5f; // Cámara lenta
        Invoke("ProcessRoundChange", 0.1f);
    }

    private void ProcessRoundChange()
    {
        Time.timeScale = 1f;
        currentRound++;

        if (currentRound > maxRounds)
        {
            EndGame();
            return;
        }

        // Reinicio completo
        player1Lives = maxLives;
        player2Lives = maxLives;
        ResetAllPlayers();
        SwapRoles();
        timer = roundDuration;
        gameEnded = false;
        UpdateUI();
    }

    private void EndRound()
    {
        
        if (currentRound > maxRounds)
        {
            EndGame();
        }
        else
        {
            player1Lives = maxLives;
            player2Lives = maxLives;
            SwapRoles();
            timer = roundDuration;
            ResetAllPlayers();
            UpdateUI();
        }
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void AssignRoles()
    {
        player1.SetRole(true);  // Jugador 1 comienza como shooter
        player2.SetRole(false); // Jugador 2 comienza como esquivador
        UpdateButtonsVisibility();
    }

    public void SwapRoles()
    {
        player1.SetRole(!player1.isShooter);
        player2.SetRole(!player2.isShooter);
        UpdateButtonsVisibility();
        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}