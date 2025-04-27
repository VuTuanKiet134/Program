using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float moveDistance = 2f; // Khoảng cách di chuyển lên xuống
    public float moveSpeed = 2f;    // Tốc độ di chuyển

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
