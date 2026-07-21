using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }

    [SerializeField] private Image faderImage;
    [SerializeField] private float fadeDuration = 1f;

    private Coroutine fadeCoroutine;

    void Awake()
    {
        Instance = this;

        // Start fully transparent, but keep the GameObject active
        SetAlpha(0f);
        faderImage.raycastTarget = false;
    }

    public void FadeToBlack()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        faderImage.raycastTarget = true; // block clicks while faded/fading in
        fadeCoroutine = StartCoroutine(FadeRoutine(1f));
    }

    public void FadeFromBlack()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeRoutine(0f));
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        float startAlpha = faderImage.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            SetAlpha(newAlpha);
            yield return null;
        }

        SetAlpha(targetAlpha);

        // once fully invisible, stop blocking clicks
        if (targetAlpha <= 0f)
            faderImage.raycastTarget = false;
    }

    private void SetAlpha(float alpha)
    {
        Color c = faderImage.color;
        c.a = alpha;
        faderImage.color = c;
    }
}