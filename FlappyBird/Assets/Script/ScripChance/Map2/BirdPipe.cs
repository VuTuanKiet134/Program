using UnityEngine;

public class BirdPipe : MonoBehaviour
{
    // Gắn vào Prefab để bật background phù hợp qua BirdOnOff.Instance

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerCam"))
        {
            if (BirdOnOff.Instance != null)
            {
                BirdOnOff.Instance.ApplyBackgroundSetting();
            }
            else
            {
                Debug.LogError("BirdOnOff.Instance is null!");
            }
        }
    }
}
