using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public float backgroundWidth = 19.14f; // chiều rộng giữa hai background (21.1 - 1.96)
    public float startX = 1.96f;
    public float spawnY = -1.42f;
    public float spawnZ = 1f; // Tọa độ z bạn yêu cầu
    public float spawnInterval = 2f; // thời gian giữa các lần spawn
    public float backgroundLifetime = 5f; // thời gian sống của mỗi background

    private float nextSpawnX;
    private float timer;

    void Start()
    {
        nextSpawnX = startX;

        // Spawn background đầu tiên
        SpawnBackground();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBackground();
            timer = 0f;
        }
    }

    void SpawnBackground()
    {
        Vector3 spawnPosition = new Vector3(nextSpawnX, spawnY, spawnZ);
        GameObject newBg = Instantiate(backgroundPrefab, spawnPosition, Quaternion.identity);

          

        // Cập nhật vị trí spawn cho lần tiếp theo
        nextSpawnX += backgroundWidth;
    }
}
