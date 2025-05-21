using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            // Tạo vụ nổ tại vị trí viên đạn
            Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);

            // Hủy viên đạn
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Laser"))
        {
            // Hủy nhân vật Sword
            Destroy(gameObject);
        }
    }
}
