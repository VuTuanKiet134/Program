using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static BlockManager Instance;
    public BlockSpawner blockSpawner;

    private int blocksRemaining = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void SetBlockCount(int count)
    {
        blocksRemaining = count;
    }

    public void BlockPlaced()
    {
        blocksRemaining--;

        if (blocksRemaining <= 0)
        {
            Debug.Log("Tạo 3 block mới...");
            blockSpawner.SpawnRandomBlocks();
        }
    }
}
