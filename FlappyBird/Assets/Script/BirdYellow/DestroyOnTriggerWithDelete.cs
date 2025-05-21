using UnityEngine;

public class DestroyOnTriggerWithDelete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Delete"))
        {
            Destroy(gameObject);
        }
    }
}
