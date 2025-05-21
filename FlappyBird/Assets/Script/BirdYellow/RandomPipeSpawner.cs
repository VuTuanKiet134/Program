using UnityEngine;

public class RandomPipeSpawner : MonoBehaviour
{
    public GameObject[] pipePrefabs;      // 0: pipe cố định, 1: pipe di chuyển
    public GameObject specialPrefab1;
    public GameObject specialPrefab2;
    public int spawnEveryN = 5;
    public int spawnEveryM = 9;

    public float pipeWidth = 5f;
    public float startX = 10f;
    public float fixedMoveY = 0f;
    public float randomMinY = -2f;
    public float randomMaxY = 2f;
    public float spawnZ = 0f;
    public float spawnInterval = 2f;

    // Tọa độ Y riêng cho 2 prefab đặc biệt
    public float special1MinY = -1f;
    public float special1MaxY = 2f;
    public float special2MinY = -2f;
    public float special2MaxY = 1f;

    private float nextSpawnX;
    private float timer;
    private int spawnCount = 0;

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
        spawnCount++;

        bool spawnSpecial1 = spawnCount % spawnEveryN == 0;
        bool spawnSpecial2 = spawnCount % spawnEveryM == 0;

        if (spawnSpecial1 && specialPrefab1 != null)
        {
            SpawnOnePipe(specialPrefab1, special1MinY, special1MaxY);
        }

        if (spawnSpecial2 && specialPrefab2 != null)
        {
            SpawnOnePipe(specialPrefab2, special2MinY, special2MaxY);
        }

        if (!spawnSpecial1 && !spawnSpecial2)
        {
            int randomIndex = Random.Range(0, pipePrefabs.Length);
            GameObject selectedPrefab = pipePrefabs[randomIndex];
            SpawnOnePipe(selectedPrefab, randomMinY, randomMaxY);
        }
    }

    void SpawnOnePipe(GameObject prefab, float minY, float maxY)
    {
        float spawnY;

        if (prefab.GetComponent<MoveUpDown>() != null)
        {
            spawnY = fixedMoveY;
        }
        else
        {
            spawnY = Random.Range(minY, maxY);
        }

        Vector3 spawnPosition = new Vector3(nextSpawnX, spawnY, spawnZ);
        Instantiate(prefab, spawnPosition, Quaternion.identity);

        nextSpawnX += pipeWidth;
    }
}
