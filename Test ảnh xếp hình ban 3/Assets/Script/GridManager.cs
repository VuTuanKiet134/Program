using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width = 8;
    public int height = 8;
    public GameObject cellPrefab;
    public Vector2 startPos = new Vector2(-350, 350);
    public float cellSpacing = 5f;

    private bool[,] grid;
    public Vector2 GetGridOrigin()
{
    RectTransform rt = GetComponent<RectTransform>();
    return rt.anchoredPosition;
}

    

    void Awake()
    {
        Instance = this;
        grid = new bool[width, height];
    }

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject cell = Instantiate(cellPrefab, transform);
                RectTransform rt = cell.GetComponent<RectTransform>();

                float cellSize = rt.sizeDelta.x;
                float posX = startPos.x + x * (cellSize + cellSpacing);
                float posY = startPos.y - y * (cellSize + cellSpacing);

                rt.anchoredPosition = new Vector2(posX, posY);
            }
        }
    }

    public bool IsCellOccupied(int x, int y)
    {
        return grid[x, y];
    }

    public void OccupyCell(int x, int y)
{
    if (x >= 0 && x < width && y >= 0 && y < height)
    {
        grid[x, y] = true;
    }
    else
    {
        Debug.LogWarning($"Tọa độ ({x}, {y}) ngoài lưới!");
    }
}


    public bool IsCellValid(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public void DebugGrid()
    {
        Debug.Log("=== Trạng thái lưới ===");
        for (int y = height - 1; y >= 0; y--)
        {
            string row = "";
            for (int x = 0; x < width; x++)
            {
                row += grid[x, y] ? "X " : ". ";
            }
            Debug.Log(row);
        }
    }
    public void PrintGrid()
{
    string gridStr = "";
    for (int y = height - 1; y >= 0; y--)
    {
        for (int x = 0; x < width; x++)
        {
            gridStr += grid[x, y] ? "X " : ". ";
        }
        gridStr += "\n";
    }

    Debug.Log("Trạng thái lưới:\n" + gridStr);
}

}