using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;   // Prefab vụ nổ
    public float destroyDelay = 1f;      // Thời gian tự hủy chính bản thân object này
    public float explosionDelay = 0.3f;  // Độ trễ trước khi tạo vụ nổ Enemy

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("🔥 Người chơi bị trúng nổ!");

            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.StartDeathSequence();
            }
            else
            {
                Destroy(collision.gameObject);
                Invoke(nameof(TriggerGameOver), 0.5f);
            }
        }
        else if (collision.CompareTag("NoBomb"))
        {
            Debug.Log("👤 Nhân vật không cầm bomb bị trúng nổ");
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            Debug.Log("💣 Nhân vật cầm bomb bị trúng nổ – tạo nổ sau delay");
            StartCoroutine(DelayedExplosion(collision.gameObject));
        }
    }

    private System.Collections.IEnumerator DelayedExplosion(GameObject enemy)
    {
        yield return new WaitForSeconds(explosionDelay);

        if (enemy != null) // Tránh lỗi nếu enemy đã bị hủy bởi lý do khác
        {
            Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
            Destroy(enemy);
        }
    }

    void TriggerGameOver()
    {
        GameManager.Instance.GameOver();
    }
}
