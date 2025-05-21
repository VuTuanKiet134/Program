using UnityEngine;

public class BackgroundDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCam"))
        {
            Destroy(gameObject);
        }
    }
}
