using UnityEngine;

public class PipeSpawnerDad : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2f;        // Tốc độ spawn bình thường
    public float firstSpawnDelay = 0.5f; // Tốc độ spawn lần đầu
    public float minY = -1f;
    public float maxY = 2f;

    private float timer;
    private bool isFirstSpawn = true;

    void Update()
    {
        if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameOver) return;

        timer += Time.deltaTime;

        float currentRate = isFirstSpawn ? firstSpawnDelay : spawnRate;

        if (timer >= currentRate)
        {
            float yOffset = Random.Range(minY, maxY);
            Vector3 spawnPos = transform.parent.position + new Vector3(0, yOffset, 0);
            Instantiate(pipePrefab, spawnPos, Quaternion.identity);
            timer = 0;
            isFirstSpawn = false;
        }
    }
}
