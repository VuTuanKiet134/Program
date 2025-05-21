using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionTrigger : MonoBehaviour
{
    [Header("Chuyển màn hình")]
    public GameObject[] allGamePlays;         // Các màn cần tắt
    public GameObject nextGamePlay;           // Màn cần bật
    public Image fadeImage;                   // Hình ảnh để làm mờ màn hình
    public float fadeDuration = 1f;           // Thời gian làm tối / sáng

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(FadeAndSwitch());
        }
    }

    IEnumerator FadeAndSwitch()
    {
        // Làm mờ dần (fade to black)
        float timer = 0f;
        Color color = fadeImage.color;
        Time.timeScale = 0;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // Chuyển màn
        foreach (GameObject go in allGamePlays)
        {
            if (go != null) go.SetActive(false);
        }

        if (nextGamePlay != null) nextGamePlay.SetActive(true);

        // Khôi phục thời gian
        Time.timeScale = 1;

        // Làm sáng dần (fade from black)
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (timer / fadeDuration));
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }
}
