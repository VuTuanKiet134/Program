using UnityEngine;

public class LaserFadeOut : MonoBehaviour
{
    public float duration = 1f; // thời gian tồn tại và mờ dần

    private float timer = 0f;
    private SpriteRenderer sr;
    private Color originalColor;

    void Start()
    {
        // Hỗ trợ cả trường hợp SpriteRenderer nằm ở con
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }

        if (sr != null)
        {
            originalColor = sr.color;
        }
        else
        {
            Debug.LogWarning("LaserFadeOut: SpriteRenderer not found!");
        }
    }

    void Update()
    {
        if (sr == null) return;

        timer += Time.deltaTime;

        float alpha = Mathf.Lerp(1f, 0f, timer / duration);
        Color newColor = originalColor;
        newColor.a = alpha;
        sr.color = newColor;

        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
