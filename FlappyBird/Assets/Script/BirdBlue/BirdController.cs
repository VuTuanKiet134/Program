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
        // Nếu đã thua -> click chuột hoặc nhấn Space để chơi lại
        if (isDead && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Nếu chưa thua
        if (!isDead)
        {
            // Click hoặc Space lần đầu để bắt đầu game
            if (!isGameStarted && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
            {
                StartGame();
            }

            // Click hoặc Space để bay lên
            if (isGameStarted && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
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
