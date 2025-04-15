using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class BlockSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs; // Gán prefab vào đây trong Inspector
    public Transform[] spawnPoints;   // Vị trí spawn 3 block

    private void Start()
    {
        SpawnRandomBlocks();
    }

   void SpawnRandomBlocks()
{
    if (blockPrefabs.Length < 3 || spawnPoints.Length < 3)
    {
        Debug.LogError("Thiếu prefab hoặc spawn point!");
        return;
    }

    GameObject[] shuffledPrefabs = blockPrefabs.OrderBy(x => Random.value).ToArray();

    for (int i = 0; i < 3; i++)
    {
        GameObject block = Instantiate(shuffledPrefabs[i], spawnPoints[i].position, Quaternion.identity);
block.transform.SetParent(transform, false);

// Gán canvas nếu có component DragDropBlock
DragDropBlock dragScript = block.GetComponent<DragDropBlock>();
if (dragScript != null)
{
    dragScript.canvas = GameObject.FindObjectOfType<Canvas>();
}

    }
}

}
