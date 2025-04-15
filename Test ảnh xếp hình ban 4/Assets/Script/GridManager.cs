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
    // Thêm vào đầu class GridManager
    private bool[,] gridCells; // Lưu trạng thái ô (đã bị chiếm hay chưa)
    private GameObject[,] cellBlocks; // Lưu các block đã chiếm ô (để có thể xóa khi cần)

    public Vector2 GetGridOrigin()
{
    RectTransform rt = GetComponent<RectTransform>();
    return rt.anchoredPosition;
}

    
public float cellSize;

void Awake()
{
    Instance = this;
    grid = new bool[width, height];

    RectTransform rt = cellPrefab.GetComponent<RectTransform>();
    cellSize = rt.sizeDelta.x; // Kích thước ô từ prefab
}

    void Start()
    {
        GenerateGrid();
    }
    

    void GenerateGrid()
{
    gridCells = new bool[width, height];
    cellBlocks = new GameObject[width, height];

    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            GameObject cell = Instantiate(cellPrefab, transform);
            RectTransform rt = cell.GetComponent<RectTransform>();

            float cellSize = rt.sizeDelta.x; // Giả sử ô vuông
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
        public void ClearCell(int x, int y)
{
    if (IsCellValid(x, y))
    {
        grid[x, y] = false;               // ✨ Cập nhật thêm dòng này
        gridCells[x, y] = false;

        if (cellBlocks[x, y] != null)
        {
            Destroy(cellBlocks[x, y]);
            cellBlocks[x, y] = null;
        }
    }
}


        public void SetCell(int x, int y, bool occupied, GameObject block)
{
    if (IsCellValid(x, y))
    {
        grid[x, y] = occupied;            // ✨ Đồng bộ cả grid
        gridCells[x, y] = occupied;
        cellBlocks[x, y] = block;
    }
}


public void CheckAndClearLines()
{
    // Kiểm tra và xóa từng hàng
    for (int y = 0; y < height; y++)
    {
        bool rowFull = true;

        // Kiểm tra xem hàng này có đầy không
        for (int x = 0; x < width; x++)
        {
            if (!gridCells[x, y]) // Nếu có ô trống, hàng không đầy
            {
                rowFull = false;
                break;
            }
        }

        if (rowFull)
        {
            Debug.Log($"Xóa hàng {y}");
            // Xóa các block trong hàng này
            for (int x = 0; x < width; x++)
            {
                ClearCell(x, y); // Xóa các block trong hàng này
            }
        }
    }

    // Kiểm tra và xóa từng cột
    for (int x = 0; x < width; x++)
    {
        bool colFull = true;

        // Kiểm tra xem cột này có đầy không
        for (int y = 0; y < height; y++)
        {
            if (!gridCells[x, y]) // Nếu có ô trống, cột không đầy
            {
                colFull = false;
                break;
            }
        }

        if (colFull)
        {
            Debug.Log($"Xóa cột {x}");
            // Xóa các block trong cột này
            for (int y = 0; y < height; y++)
            {
                ClearCell(x, y); // Xóa các block trong cột này
            }
        }
    }
}





}