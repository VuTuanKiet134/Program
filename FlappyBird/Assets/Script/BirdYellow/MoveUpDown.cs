using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float moveDistance = 2f; // Khoảng cách di chuyển lên xuống
    public float moveSpeed = 2f;    // Tốc độ di chuyển

    private Vector3 startPos;
    private float timeOffset;

    void Start()
    {
        startPos = transform.position;
        timeOffset = Random.Range(0f, 2 * Mathf.PI); // Lệch pha từ 0 đến 2π
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed + timeOffset) * moveDistance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
