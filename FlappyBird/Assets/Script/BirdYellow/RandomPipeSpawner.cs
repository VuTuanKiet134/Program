using UnityEngine;

public class RandomPipeSpawner : MonoBehaviour
{
    public GameObject[] pipePrefabs; // 0: pipe cố định, 1: pipe di chuyển
    public float pipeWidth = 5f; 
    public float startX = 10f;
    public float fixedMoveY = 0f;    // vị trí Y cố định cho pipe di chuyển
    public float randomMinY = -2f;   // min vị trí Y cho pipe không di chuyển
    public float randomMaxY = 2f;    // max vị trí Y cho pipe không di chuyển
    public float spawnZ = 0f;
    public float spawnInterval = 2f;

    private float nextSpawnX;
    private float timer;

    void Start()
    {
        nextSpawnX = startX;
        SpawnPipe();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPipe();
            timer = 0f;
        }
    }

    void SpawnPipe()
    {
        // Random chọn 1 trong 2 prefab
        int randomIndex = Random.Range(0, pipePrefabs.Length);
        GameObject selectedPrefab = pipePrefabs[randomIndex];

        float spawnY;

        if (selectedPrefab.GetComponent<MoveUpDown>() != null)
        {
            // Nếu prefab có script MoveUpDown => spawn tại fixedMoveY
            spawnY = fixedMoveY;
        }
        else
        {
            // Nếu prefab không có MoveUpDown => spawn ngẫu nhiên Y
            spawnY = Random.Range(randomMinY, randomMaxY);
        }

        Vector3 spawnPosition = new Vector3(nextSpawnX, spawnY, spawnZ);
        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        nextSpawnX += pipeWidth;
    }
}
