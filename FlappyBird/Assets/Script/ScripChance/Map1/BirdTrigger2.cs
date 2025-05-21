using UnityEngine;

public class BirdTrigger2 : MonoBehaviour
{
    // Tắt bật Bird thông qua BirdTurnOff2

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerCam"))
        {
            if (BirdTurnOff2.Instance != null)
            {
                BirdTurnOff2.Instance.ApplyBackgroundSetting();
            }
            else
            {
                Debug.LogError("BirdTurnOff2.Instance is null!");
            }
        }
    }
}
