using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            // Tạo hiệu ứng nổ tại vị trí hiện tại
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Hủy đạn
            Destroy(collision.gameObject);

            // Hủy kẻ địch
            Destroy(gameObject);
        }
        if (collision.CompareTag("Laser"))
        {
            // Tạo hiệu ứng nổ tại vị trí hiện tại
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            

            // Hủy kẻ địch
            Destroy(gameObject);
        }
    }
}
