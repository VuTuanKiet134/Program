using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDropBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Canvas canvas;

    public Vector2 gridOrigin = new Vector2(-350, 350);  // Gốc lưới (nên điều chỉnh theo layout của bạn)
    public float cellSize = 100f; // Kích thước 1 ô vuông

    private Vector2 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
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
        // Tính vị trí hiện tại của block so với gốc lưới
        Vector2 localPos = rectTransform.anchoredPosition;
        Vector2 relativePos = localPos - gridOrigin;

        // Làm tròn để snap
        int snapX = Mathf.RoundToInt(relativePos.x / cellSize);
        int snapY = Mathf.RoundToInt(relativePos.y / cellSize);

        // Tính lại vị trí snapped
        Vector2 snappedPos = new Vector2(snapX * cellSize, snapY * cellSize) + gridOrigin;

        // Cập nhật lại vị trí block
        rectTransform.anchoredPosition = snappedPos;

        canvasGroup.blocksRaycasts = true;

        Debug.Log($"Snap vào ô ({snapX}, {snapY}) tại {snappedPos}");
    }
}