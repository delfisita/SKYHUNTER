using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

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

    [Header("Corazones de vida")]
    public Image[] player1Hearts; // Corazones del jugador 1
    public Image[] player2Hearts; // Corazones del jugador 2

    [Header("Interfaz de Usuario")]
    public Text timerText;
    public Text roundText;
    public GameObject gameOverPanel;
    public TMP_Text winnerText;
    public Button restartButton;

    [Header("Power-Up")]
    public InvincibilityPowerUpManager powerUpManager;

    private float timer;
    private int currentRound = 1;
    private bool gameEnded = false;
    private int player1Lives;
    private int player2Lives;
    private int player1Wins = 0;
    private int player2Wins = 0;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        player1ShootButton.SetActive(false);
        player1LeftButton.SetActive(false);
        player1RightButton.SetActive(false);
        player2ShootButton.SetActive(false);
        player2LeftButton.SetActive(false);
        player2RightButton.SetActive(false);

        player1Lives = maxLives;
        player2Lives = maxLives;

        InitializeGame();
        restartButton.onClick.AddListener(RestartGame);

        StartCoroutine(ShowPowerUpButtonRoutine());
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
        if (playerID == 1) player1Lives--;
        else if (playerID == 2) player2Lives--;

        UpdateUI();

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
    }

    private void ResetAllPlayers()
    {
        player1Health.ResetPlayer();
        player2Health.ResetPlayer();
    }

    private void UpdateUI()
    {
        timerText.text = $"Tiempo: {Mathf.CeilToInt(timer)}";
        roundText.text = $"Ronda: {currentRound}/{maxRounds}";

        for (int i = 0; i < player1Hearts.Length; i++)
        {
            player1Hearts[i].gameObject.SetActive(i < player1Lives);
        }

        for (int i = 0; i < player2Hearts.Length; i++)
        {
            player2Hearts[i].gameObject.SetActive(i < player2Lives);
        }
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

    public void HandlePlayerDeath()
    {
        gameEnded = true;

        if (player1Lives <= 0) player2Wins++;
        else if (player2Lives <= 0) player1Wins++;

        Invoke("ProcessRoundChange", 0.01f);
    }

    private void ProcessRoundChange()
    {
        Time.timeScale = 1f;
        SwapRoles();

        currentRound++;

        if (currentRound > maxRounds)
        {
            EndGame();
            return;
        }

        player1Lives = maxLives;
        player2Lives = maxLives;
        ResetAllPlayers();
        timer = roundDuration;
        gameEnded = false;
        UpdateUI();
    }

    private void EndRound()
    {
        if (player1.isShooter) player2Wins++;
        else player1Wins++;

        currentRound++;
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

        if (player1Wins > player2Wins)
            winnerText.text = "¡Jugador 1 Gana!";
        else if (player2Wins > player1Wins)
            winnerText.text = "¡Jugador 2 Gana!";

        SavePoints(50);
    }

    private void SavePoints(int pointsToAdd)
    {
        int currentPoints = PlayerPrefs.GetInt("TotalPoints", 0);
        currentPoints += pointsToAdd;
        PlayerPrefs.SetInt("TotalPoints", currentPoints);
        PlayerPrefs.Save();
    }

    public void AssignRoles()
    {
        player1.SetRole(true);
        player2.SetRole(false);
        UpdateButtonsVisibility();

        if (powerUpManager != null)
            powerUpManager.ResetButton();
    }

    public void SwapRoles()
    {
        player1.SetRole(!player1.isShooter);
        player2.SetRole(!player2.isShooter);
        UpdateButtonsVisibility();

        if (powerUpManager != null)
            powerUpManager.ResetButton();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    private IEnumerator ShowPowerUpButtonRoutine()
    {
        while (currentRound <= maxRounds)
        {
            yield return new WaitForSeconds(10f);

            if (powerUpManager != null)
            {
                powerUpManager.ShowButtonNow();
            }

           
            yield return new WaitForSeconds(roundDuration - 20f);
        }
    }
}
