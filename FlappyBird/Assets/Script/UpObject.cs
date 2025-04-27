using UnityEngine;

public class UpObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Giả sử Fish của bạn có tag là "Player"
        {
            // Ẩn thanh dọc
            gameObject.SetActive(false);

            // (Tuỳ chọn) Nếu bạn muốn phá huỷ luôn, thay bằng:
            // Destroy(gameObject);
        }
    }
}
