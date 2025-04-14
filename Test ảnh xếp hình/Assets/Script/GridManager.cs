using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public int width = 8;
    public int height = 8;
    public GameObject cellPrefab;
    public Vector2 startPos = new Vector2(-350, 350); // Điểm bắt đầu của lưới
    public float cellSpacing = 5f;

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

                float cellSize = rt.sizeDelta.x; // Giả sử ô vuông
                float posX = startPos.x + x * (cellSize + cellSpacing);
                float posY = startPos.y - y * (cellSize + cellSpacing);

                rt.anchoredPosition = new Vector2(posX, posY);
            }
        }
    }
}