using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float destroyX = -10f;

    void Update()
    {
        // Chỉ hoạt động khi game bắt đầu
        if (!GameManager.Instance.IsGameStarted) return;

        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
