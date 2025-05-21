using UnityEngine;

public class SpawnTwo : MonoBehaviour
{
    public GameObject pipePrefab;
    public GameObject specialPrefab1;
    public GameObject specialPrefab2;

    public float spawnRate = 2f;

    // Tọa độ Y cho pipePrefab
    public float minY = -1f;
    public float maxY = 2f;

    // Tọa độ Y cho specialPrefab1
    public float minY1 = 0f;
    public float maxY1 = 3f;

    // Tọa độ Y cho specialPrefab2
    public float minY2 = -2f;
    public float maxY2 = 1f;

    public int spawnInterval1 = 5; // n
    public int spawnInterval2 = 10; // m

    private float timer;
    private int spawnCount = 0;

    void Update()
    {
        if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameOver) return;

        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            spawnCount++;

            bool spawn1 = (spawnInterval1 > 0 && spawnCount % spawnInterval1 == 0);
            bool spawn2 = (spawnInterval2 > 0 && spawnCount % spawnInterval2 == 0);

            // Spawn pipePrefab nếu không trùng special
            if (!spawn1 && !spawn2)
            {
                float y = Random.Range(minY, maxY);
                Vector3 spawnPos = new Vector3(transform.position.x, y, 0);
                Instantiate(pipePrefab, spawnPos, Quaternion.identity);
            }

            // Spawn specialPrefab1
            if (spawn1)
            {
                float y1 = Random.Range(minY1, maxY1);
                Vector3 pos1 = new Vector3(transform.position.x, y1, 0);
                Instantiate(specialPrefab1, pos1, Quaternion.identity);
            }

            // Spawn specialPrefab2
            if (spawn2)
            {
                float y2 = Random.Range(minY2, maxY2);
                Vector3 pos2 = new Vector3(transform.position.x, y2, 0);
                Instantiate(specialPrefab2, pos2, Quaternion.identity);
            }

            timer = 0;
        }
    }
}
