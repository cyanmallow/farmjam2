using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class FadeScreen : MonoBehaviour
{
    public static FadeScreen Instance { get; private set; }
    [SerializeField] private float fadeDuration = 1f;
    private UIDocument fadeDocument;
    private VisualElement fadeOverlay;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        fadeDocument = GetComponent<UIDocument>();
        fadeOverlay = fadeDocument.rootVisualElement.Q<VisualElement>("BlackScreen");
        fadeOverlay.pickingMode = PickingMode.Ignore; // allow input when not fading
        fadeOverlay.style.visibility = Visibility.Hidden;
    }
    public void FadeToBlack(Action onComplete = null) => StartCoroutine(Fade(0f, 1f, onComplete));
    public void FadeFromBlack(Action onComplete = null) => StartCoroutine(Fade(1f, 0f, onComplete));

    private IEnumerator Fade(float from, float to, Action onComplete)
    {
        fadeOverlay.pickingMode = to > from ? PickingMode.Position : PickingMode.Ignore; // block input while fading to black
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            fadeOverlay.style.opacity = Mathf.Lerp(from, to, t);
            yield return null;
        }
        fadeOverlay.style.opacity = to;
        if (to == 0f) fadeOverlay.pickingMode = PickingMode.Ignore;
        onComplete?.Invoke();
    }
}
