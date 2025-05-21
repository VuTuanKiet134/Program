using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [Header("Giá trị X tối thiểu trước khi bị hủy")]
    public float destroyXThreshold = -10f;

    void Update()
    {
        if (transform.position.x > destroyXThreshold)
        {
            Destroy(gameObject);
        }
    }
}
