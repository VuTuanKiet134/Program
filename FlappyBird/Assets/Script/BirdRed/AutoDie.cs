using UnityEngine;

public class AutoDie : MonoBehaviour
{
    [Header("Cài đặt")]
    public GameObject explosionPrefab;     // Prefab vụ nổ
    public float triggerX = -10f;          // Khi nhân vật đến vị trí X nhỏ hơn hoặc bằng, sẽ nổ
    public bool triggerOnce = true;        // Chỉ nổ một lần hay không

    private bool hasTriggered = false;

    void Update()
    {
        if (!hasTriggered && transform.position.x <= triggerX)
        {
            TriggerExplosion();

            if (triggerOnce)
            {
                hasTriggered = true;
            }
        }
    }

    void TriggerExplosion()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Debug.Log("💥 AutoDie: Nhân vật chạm trigger X và phát nổ!");
        }
        else
        {
            Debug.LogWarning("⚠️ AutoDie: Chưa gán prefab vụ nổ!");
        }

        Destroy(gameObject); // Hủy luôn nhân vật sau khi nổ
    }
}
