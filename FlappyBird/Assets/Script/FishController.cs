using UnityEngine;
using UnityEngine.SceneManagement;

public class FishController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float smoothTime = 0.1f;
    public float rotationSpeed = 5f;
    public float maxRotationAngle = 30f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPosition;
    private Vector3 lastMousePosition;
    private bool isDragging = false;
    private bool isDead = false;

    [Header("Level Switching")]
    public GameObject easySpawner;       // kéo Spawner dễ vào đây
    public GameObject hardSpawner;       // kéo Spawner khó vào đây
    public GameObject extraObjectToDisable; // kéo thêm Object cần tắt vào đây

    private Vector2 screenBoundsMin;
    private Vector2 screenBoundsMax;

    void Start()
    {
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        screenBoundsMin = new Vector2(lowerLeft.x, lowerLeft.y);
        screenBoundsMax = new Vector2(upperRight.x, upperRight.y);

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

            targetPosition.x = Mathf.Clamp(targetPosition.x, screenBoundsMin.x, screenBoundsMax.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, screenBoundsMin.y, screenBoundsMax.y);

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LevelTrigger"))
        {
            SwitchToHardLevel();
        }
    }

    void SwitchToHardLevel()
    {
        if (easySpawner != null) easySpawner.SetActive(false);
        if (hardSpawner != null) hardSpawner.SetActive(true);
        if (extraObjectToDisable != null) extraObjectToDisable.SetActive(false); // ➡️ TẮT thêm object khác
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
