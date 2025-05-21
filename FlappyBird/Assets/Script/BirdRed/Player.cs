using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject deathEffect;

    public void StartDeathSequence()
    {
        Debug.Log("🔥 Người chơi bắt đầu trình tự chết");

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        gameObject.SetActive(false); // Ẩn người chơi

        Invoke(nameof(TriggerGameOver), 0.5f); // Gọi thua game sau 0.5s
    }

    private void TriggerGameOver()
    {
        GameManager.Instance.GameOver();
    }
}
