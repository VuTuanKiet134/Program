using UnityEngine;

public class BirdRedController : MonoBehaviour
{
    [Header("Movement & Rotation")]
    public Transform rocketTransform;
    public float rocketMaxRotation = 90f;
    public float moveSpeed = 5f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public GameObject laserPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float laserHoldTime = 2f; // 👉 Cho phép chỉnh thời gian giữ chuột để bắn laser

    private Camera mainCam;

    private Vector3 startMouseWorldPos;
    private bool isDragging = false;
    private float mouseDownTime;
    private bool isLaserFired = false;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.StartGame();
            }
            return;
        }

        if (GameManager.Instance.IsGameOver) return;

        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;
            mouseDownTime = Time.time;
            isLaserFired = false;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            float deltaY = currentMousePos.y - startMouseWorldPos.y;

            if (!isDragging && Mathf.Abs(deltaY) > 0.05f)
            {
                isDragging = true;
            }

            if (isDragging)
            {
                RotateRocket(currentMousePos);
                MoveBird(deltaY);
            }
            else
            {
                // Bắn laser nếu giữ đủ thời gian
                if (!isLaserFired && Time.time - mouseDownTime >= laserHoldTime)
                {
                    FireLaser();
                    isLaserFired = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isDragging && !isLaserFired)
            {
                FireBullet();
            }

            isDragging = false;
        }

        if (!Input.GetMouseButton(0))
        {
            ReturnRocketToDefault();
        }
    }

    void RotateRocket(Vector3 currentMousePos)
    {
        Vector3 direction = rocketTransform.position - currentMousePos;
        direction.z = 0f;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -rocketMaxRotation, rocketMaxRotation);

        rocketTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void ReturnRocketToDefault()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
        rocketTransform.rotation = Quaternion.Lerp(rocketTransform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    void MoveBird(float deltaY)
    {
        float directionY = deltaY > 0 ? -1f : 1f;
        Vector3 movement = new Vector3(0, directionY, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;

        ClampToScreen();
    }

    void ClampToScreen()
    {
        Vector3 pos = transform.position;
        Vector3 viewportPos = mainCam.WorldToViewportPoint(pos);

        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.05f, 0.95f);

        transform.position = mainCam.ViewportToWorldPoint(viewportPos);
    }


    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = firePoint.right * bulletSpeed;
        }

        Debug.Log("🔫 Bullet Fired!");
    }

    void FireLaser()
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("🔴 Laser Fired!");
    }
    
    void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("NoBomb"))
    {
        Debug.Log("💥 Chạm vật thể NoBomb - Game Over!");
        GameManager.Instance.GameOver();
    }
}

}
