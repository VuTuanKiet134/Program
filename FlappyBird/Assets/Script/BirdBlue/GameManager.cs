using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsGameStarted { get; private set; }
    public bool IsGameOver { get; private set; }

    [Header("Game Over Display")]
    public GameObject gameOverSprite; // Gán ảnh ở đây

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Time.timeScale = 1f;

        if (gameOverSprite != null)
            gameOverSprite.SetActive(false); // Ẩn lúc đầu
    }

    public void StartGame()
    {
        IsGameStarted = true;
        IsGameOver = false;
        Debug.Log("Game Started!");
    }

    public void GameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("Game Over!");

        if (gameOverSprite != null)
            gameOverSprite.SetActive(true); // ✅ Hiện ảnh ra
    }
}

