using UnityEngine;
using UnityEngine.UI; // Needed for UI elements
using UnityEngine.SceneManagement; // Needed to restart the game

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Spawner Settings")]
    public GameObject zombiePrefab;
    public int totalZombies = 10;
    public float minSpawnRadius = 10f; 
    public float maxSpawnRadius = 15f; 

    [Header("Game Rules")]
    public float gameDuration = 60f; // The configurable timer!
    private float timeRemaining;
    private int score = 0;
    private bool isGameOver = false;

    [Header("UI Connections")]
    public Text scoreText;
    public Text timerText;
    public Text fpsText;
    public GameObject gameOverPanel;
    public Text finalScoreText;

    private float deltaTime = 0.0f; // Used to calculate exact FPS

    void Awake()
    {
        if (Instance == null) Instance = this;
        Time.timeScale = 1f; // Make sure time is flowing when the game starts
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        timeRemaining = gameDuration;
        gameOverPanel.SetActive(false); // Hide the end screen
        UpdateUI();

        for (int i = 0; i < totalZombies; i++)
        {
            SpawnZombie();
        }
    }

    void Update()
    {
        if (isGameOver) return;

        // 1. Countdown the Timer
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            EndGame();
        }

        // 2. Calculate and display FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();

        UpdateUI();
    }

    public void SpawnZombie()
    {
        // 1. Pick a random direction (a point on the edge of a circle)
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        
        // 2. Pick a random distance between our Minimum and Maximum
        float randomDistance = Random.Range(minSpawnRadius, maxSpawnRadius);
        
        // 3. Multiply the direction by the distance to get the final spot!
        Vector2 randomPos = randomDirection * randomDistance;
        Vector3 spawnPosition = new Vector3(randomPos.x, 0f, randomPos.y);

        // Spawn the zombie facing a random direction
        Instantiate(zombiePrefab, spawnPosition, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
    }

    public void ZombieHit()
    {
        if (isGameOver) return;
        
        score++; // Give the player a point!
        UpdateUI();
        
        Invoke("SpawnZombie", 2f); // Spawn a replacement
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score.ToString();
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
    }

    void EndGame()
    {
        isGameOver = true;
        Time.timeScale = 0f; // This freezes the physics, stopping the car and zombies instantly!
        gameOverPanel.SetActive(true); // Show the Game Over screen
        finalScoreText.text = "Final Score: " + score.ToString();
    }

    // The Restart Button will call this function
    public void RestartGame()
    {
        Time.timeScale = 1f; // Unfreeze time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the level
    }
}