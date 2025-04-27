using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdController : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isDead = false;
    private bool isGameStarted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false; // Ngăn vật lý hoạt động trước khi bắt đầu
    }

    void Update()
    {
        // Khi đã thua -> bấm Space để chơi lại
        if (isDead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Nếu chưa thua
        if (!isDead)
        {
            // Nhấn space lần đầu tiên để bắt đầu game
            if (!isGameStarted && Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }

            // Bay lên khi game đã bắt đầu
            if (isGameStarted && Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        }
    }

    void StartGame()
    {
        isGameStarted = true;
        rb.simulated = true;
        GameManager.Instance.StartGame();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Pipe") || collision.collider.CompareTag("Ground"))
        {
            if (!isDead)
            {
                rb.velocity = Vector2.zero;
                rb.simulated = false;
                isDead = true;
                GameManager.Instance.GameOver();
            }
        }
    }
}
