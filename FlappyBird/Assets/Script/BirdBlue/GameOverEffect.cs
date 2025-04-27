using UnityEngine;

public class GameOverEffect : MonoBehaviour
{
    public float animationDuration = 0.5f;
    public float scaleUp = 1.2f;
    public float scaleDown = 1f;

    void OnEnable()
    {
        // Khi GameObject được bật, bắt đầu hiệu ứng
        transform.localScale = Vector3.zero;
        StartCoroutine(PlayAnimation());
    }

    System.Collections.IEnumerator PlayAnimation()
    {
        float timer = 0f;

        // Scale up
        while (timer < animationDuration)
        {
            float t = timer / animationDuration;
            float scale = Mathf.Lerp(0f, scaleUp, t);
            transform.localScale = new Vector3(scale, scale, 1f);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(scaleUp, scaleUp, 1f);

        // Scale down
        timer = 0f;
        while (timer < animationDuration / 2)
        {
            float t = timer / (animationDuration / 2);
            float scale = Mathf.Lerp(scaleUp, scaleDown, t);
            transform.localScale = new Vector3(scale, scale, 1f);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localScale = new Vector3(scaleDown, scaleDown, 1f);
    }
}
