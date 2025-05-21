using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdYellowController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float horizontalSpeed = 5f;

    private Rigidbody2D rb;
    private bool isDead = false;
    private bool isGameStarted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }

    void Update()
    {
        if (isDead)
        {
            // Click chuột hoặc nhấn Space để chơi lại
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return;
        }

        // Bắt đầu game bằng chuột hoặc phím Space
        if (!isGameStarted && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            StartGame();
        }

        if (isGameStarted)
        {
            float verticalSpeed = rb.velocity.y;

            // Giữ chuột hoặc giữ phím Space để bay lên
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
            {
                verticalSpeed = jumpForce;
            }

            rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
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
