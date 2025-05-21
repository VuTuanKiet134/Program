using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Nếu chạm vào Player thì Game Over, KHÔNG hủy mũi tên
        if (collision.CompareTag("Player"))
        {
            
            GameManager.Instance.GameOver();
            return;
        }

        // Nếu chạm vào Bullet thì hủy cả mũi tên và viên đạn
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // hủy Bullet
            Destroy(gameObject); // hủy Arrow
            return;
        }

        // Nếu chạm vào Laser thì chỉ hủy mũi tên
        if (collision.CompareTag("Laser"))
        {
            Destroy(gameObject);
            return;
        }

        // Nếu không phải NoBomb thì hủy mũi tên
        
    }
}
