using UnityEngine;

public class LoopingObject : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float resetX = -20f;  
    public float startX = 20f;   

    void Update()
    {
        
        if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsGameOver) return;

        // Di chuyển sang trái
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        
        if (transform.position.x <= resetX)
        {
            Vector3 newPos = transform.position;
            newPos.x = startX;
            transform.position = newPos;
        }
    }
}
