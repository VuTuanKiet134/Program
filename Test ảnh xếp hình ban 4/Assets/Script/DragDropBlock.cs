using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDropBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Canvas canvas;

    public Vector2 gridOrigin = new Vector2(-350, 350);  // Gốc lưới
     // Kích thước 1 ô vuông

    private Vector2 startPosition;

    private float cellSize;

void Awake()
{
    rectTransform = GetComponent<RectTransform>();
    canvasGroup = GetComponent<CanvasGroup>();
    canvas = GetComponentInParent<Canvas>();

    gridOrigin = GridManager.Instance.GetGridOrigin();
    cellSize = GridManager.Instance.cellSize; // ✨ lấy từ GridManager
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

        for (int i = 0; i < blockScript.cellOffsets.Length; i++)
{
    int cellX = snapX + blockScript.cellOffsets[i].x;
    int cellY = snapY + blockScript.cellOffsets[i].y;

    Transform cell = blockScript.transform.GetChild(i);

    GridManager.Instance.OccupyCell(cellX, cellY);
    GridManager.Instance.SetCell(cellX, cellY, true, cell.gameObject);
}


        GridManager.Instance.PrintGrid();
         GridManager.Instance.CheckAndClearLines();

        // Cho phép raycast trở lại (phòng khi có thao tác UI khác)
        canvasGroup.blocksRaycasts = true;
        Transform placedParent = GameObject.Find("PlacedBlocksContainer").transform;
        transform.SetParent(placedParent, false); 
        // Gán block về container mới


        // Gỡ bỏ tính năng kéo thả để block không di chuyển nữa
        Destroy(this); // Gỡ script DragDropBlock
        Destroy(canvasGroup); // Gỡ luôn CanvasGroup nếu không cần

        // Thông báo đã đặt block thành công
        BlockManager.Instance.BlockPlaced();
    }
    else
    {
        // Nếu không hợp lệ thì trả lại chỗ cũ
        rectTransform.anchoredPosition = startPosition;
        canvasGroup.blocksRaycasts = true;
    }

    Debug.Log($"Block localPos: {localPos}, gridOrigin: {gridOrigin}, relativePos: {relativePos}, snap: ({snapX}, {snapY})");
}


}