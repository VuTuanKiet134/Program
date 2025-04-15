using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDropBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Canvas canvas;

    public Vector2 gridOrigin = new Vector2(-350, 350);  // Gốc lưới
    public float cellSize = 100f; // Kích thước 1 ô vuông

    private Vector2 startPosition;

    private void Awake()
{
    rectTransform = GetComponent<RectTransform>();
    canvasGroup = GetComponent<CanvasGroup>();
    canvas = GetComponentInParent<Canvas>();

    // Lấy gridOrigin từ GridManager (qua RectTransform)
    gridOrigin = GridManager.Instance.GetGridOrigin();
}


    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 localPos = rectTransform.anchoredPosition;
        Vector2 relativePos = localPos - gridOrigin;

        int snapX = Mathf.RoundToInt(relativePos.x / cellSize);
        int snapY = Mathf.RoundToInt(relativePos.y / cellSize);

        Block blockScript = GetComponent<Block>();
        bool canPlace = true;

        foreach (Vector2Int offset in blockScript.cellOffsets)
        {
            int cellX = snapX + offset.x;
            int cellY = snapY + offset.y;

            if (!GridManager.Instance.IsCellValid(cellX, cellY) ||
                GridManager.Instance.IsCellOccupied(cellX, cellY))
            {
                canPlace = false;
                Debug.Log($"Không thể gắn vào ô ({cellX}, {cellY})");
                break;
            }
        }

        if (canPlace)
        {
            Vector2 snappedPos = new Vector2(snapX * cellSize, snapY * cellSize) + gridOrigin;
            rectTransform.anchoredPosition = snappedPos;

            foreach (Vector2Int offset in blockScript.cellOffsets)
            {
                int cellX = snapX + offset.x;
                int cellY = snapY + offset.y;

                if (GridManager.Instance.IsCellValid(cellX, cellY))
                {
                    GridManager.Instance.OccupyCell(cellX, cellY);
                }
                GridManager.Instance.OccupyCell(cellX, cellY);

        // In ra các ô đã được chiếm
        Debug.Log($"Block đã gắn vào ô ({cellX}, {cellY})");
            }
            GridManager.Instance.PrintGrid();
            GridManager.Instance.DebugGrid();

            canvasGroup.blocksRaycasts = true;
            Destroy(this);
        }
        else
        {
            rectTransform.anchoredPosition = startPosition;
            canvasGroup.blocksRaycasts = true;
        }
        Debug.Log($"Block localPos: {localPos}, gridOrigin: {gridOrigin}, relativePos: {relativePos}, snap: ({snapX}, {snapY})");

    }
}