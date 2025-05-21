using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float smoothSpeed = 0.725f;
    public Vector3 offset;

    private Transform target;

    void Start()
    {
        Invoke(nameof(FindActiveTarget), 0.1f); // Delay nhẹ để map có thời gian kích hoạt
    }

    void FixedUpdate()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
            return;

        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y + offset.y, -10);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void FindActiveTarget()
{
    GameObject[] candidates = GameObject.FindGameObjectsWithTag("PlayerCam");

    foreach (GameObject go in candidates)
    {
        if (go.activeInHierarchy)
        {
            target = go.transform;
            Debug.Log("📸 Camera đã gán target: " + go.name);
            return;
        }
    }

    // Nếu không tìm thấy PlayerCam thì không có target
    target = null;
    // Đưa camera về vị trí mặc định
    transform.position = new Vector3(0f, 0f, -10f);
    Debug.Log("⚠️ Không tìm thấy PlayerCam, camera về vị trí mặc định (0,0,-10)");
}


}
