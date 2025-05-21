using UnityEngine;

public class BirdPipe2 : MonoBehaviour
{
    // Gắn vào Prefab để bật background phù hợp qua BirdOnOff2.Instance

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerCam"))
        {
            if (BirdOnOff2.Instance != null)
            {
                BirdOnOff2.Instance.ApplyBackgroundSetting();
            }
            else
            {
                Debug.LogError("BirdOnOff2.Instance is null!");
            }
        }
    }
}
