using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2f;
    public float minY = -1f;
    public float maxY = 2f;

    private float timer;
    

    void Update()
{
    if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameOver) return;

    timer += Time.deltaTime;
    if (timer >= spawnRate)
    {
        float y = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(transform.position.x, y, 0);
        Instantiate(pipePrefab, spawnPos, Quaternion.identity);
        timer = 0;
    }
}


}
