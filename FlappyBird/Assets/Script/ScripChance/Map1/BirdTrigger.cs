using UnityEngine;

public class BirdTrigger : MonoBehaviour
{
    // tat bat Bird

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerCam"))
        {
            if (BirdTurnOff.Instance != null)
            {
                BirdTurnOff.Instance.ApplyBackgroundSetting();
            }
            else
            {
                Debug.LogError("BirdTurnOff.Instance is null!");
            }
        }
    }
}
