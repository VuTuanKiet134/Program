using UnityEngine;
using UnityEngine.SceneManagement;

public class FishController : MonoBehaviour
{
    [Header("Giới hạn di chuyển")]
public Vector2 minBound = new Vector2(-5f, -3f); // Tọa độ thấp nhất
public Vector2 maxBound = new Vector2(5f, 3f);   // Tọa độ cao nhất

    public float moveSpeed = 5f;
    public float smoothTime = 0.1f;
    public float rotationSpeed = 5f;
    public float maxRotationAngle = 30f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private Vector3 lastMousePosition;
    private bool isDragging = false;
    private bool isDead = false;

    private Vector2 screenBoundsMin;
    private Vector2 screenBoundsMax;

    void Start()
    {
        
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isDead)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RestartGame();
            }
            return;
        }

        HandleInput();
        HandleRotation();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameManager.Instance.IsGameStarted && !isDead)
            {
                GameManager.Instance.StartGame();
            }

            isDragging = true;
            lastMousePosition = Input.mousePosition;
            targetPosition = transform.position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging && GameManager.Instance.IsGameStarted && !GameManager.Instance.IsGameOver && !isDead)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 worldDelta = Camera.main.ScreenToWorldPoint(currentMousePosition) - Camera.main.ScreenToWorldPoint(lastMousePosition);
            worldDelta.z = 0f;

            targetPosition += worldDelta * moveSpeed;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minBound.x, maxBound.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBound.y, maxBound.y);


            lastMousePosition = currentMousePosition;
        }

        if (GameManager.Instance.IsGameStarted && !GameManager.Instance.IsGameOver && !isDead)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    void HandleRotation()
    {
        if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameOver || isDead) return;

        float verticalSpeed = (targetPosition.y - transform.position.y) / Time.deltaTime;

        float targetAngle = Mathf.Clamp(verticalSpeed * 0.05f, -maxRotationAngle, maxRotationAngle);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && (collision.collider.CompareTag("Pipe") || collision.collider.CompareTag("Ground")))
        {
            isDead = true;
            GameManager.Instance.GameOver();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
