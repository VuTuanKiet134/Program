using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsGameStarted { get; private set; }
    public bool IsGameOver { get; private set; }
    

    private void Awake()
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

    private void Update()
    {
        // Bắt đầu game khi click chuột hoặc nhấn Space lần đầu
        if (!IsGameStarted && !IsGameOver)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }

        // Nếu thua, click chuột hoặc nhấn Space sẽ restart game
        if (IsGameOver && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            RestartGame();
        }
    }
    public void ResetGameState()
{
    IsGameStarted = false;
    IsGameOver = false;
    Time.timeScale = 1f;
     Debug.Log("🔁 Reset trạng thái game: chờ người chơi ấn Space");
}


    public void StartGame()
    {
        IsGameStarted = true;
        IsGameOver = false;
        Time.timeScale = 1f;
        Debug.Log("🎮 Game bắt đầu!");
    }

    public void GameOver()
    {
        if (IsGameOver) return;

        IsGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("☠️ Game Over! Tất cả đã dừng lại.");
    }

    public void PauseGame()
    {
        IsGameStarted = false;
        Time.timeScale = 0f;
        Debug.Log("⏸️ Game đã tạm dừng khi chuyển map.");
    }

    public void RestartGame()
    {
        Debug.Log("🔄 Restarting game...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
