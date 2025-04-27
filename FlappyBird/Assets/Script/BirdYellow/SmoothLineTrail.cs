using System.Collections.Generic;
using UnityEngine;

public class SmoothLineTrail : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float minDistance = 0.1f;
    public int maxPositions = 50;

    private List<Vector3> points = new List<Vector3>();

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        points.Add(transform.position);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    void Update()
    {
        Vector3 currentPos = transform.position;

        // Chỉ thêm nếu di chuyển đủ xa
        if (Vector3.Distance(points[points.Count - 1], currentPos) > minDistance)
        {
            points.Add(currentPos);

            // Giới hạn số lượng điểm
            if (points.Count > maxPositions)
            {
                points.RemoveAt(0);
            }

            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }
}
