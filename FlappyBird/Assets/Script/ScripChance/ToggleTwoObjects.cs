using UnityEngine;

public class ToggleTwoObjects : MonoBehaviour
{
    // tat bat BackGround
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerCam"))
        {
            if (BackGroundManager.Instance != null)
            {
                BackGroundManager.Instance.ToggleBackgrounds();
            }
            else
            {
                Debug.LogError("BackGroundManager.Instance is null!");
            }
        }
    }
}
