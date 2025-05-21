using UnityEngine;

public class ExplosionAutoDestroy : MonoBehaviour
{
    [Tooltip("Thời gian tự hủy hiệu ứng nổ (giây)")]
    public float destroyDelay = 1f;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
