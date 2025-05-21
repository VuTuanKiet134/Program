using UnityEngine;

public class FadeAndDisappear : MonoBehaviour
{
    public float fadeDuration = 2f;      // Thời gian để mờ dần (giây)
    public bool destroyAfterFade = true; // True: xóa đối tượng, False: chỉ tắt (SetActive false)

    private Renderer rend;
    private Color originalColor;
    private float fadeTimer;

    void Start()
    {
        rend = GetComponent<Renderer>();

        if (rend == null)
        {
            Debug.LogError("Không tìm thấy Renderer!");
            enabled = false;
            return;
        }

        originalColor = rend.material.color;
        fadeTimer = fadeDuration;
    }

    void Update()
    {
        if (fadeTimer > 0f)
        {
            fadeTimer -= Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);

            Color newColor = originalColor;
            newColor.a = alpha;
            rend.material.color = newColor;

            if (fadeTimer <= 0f)
            {
                if (destroyAfterFade)
                {
                    Destroy(gameObject);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
