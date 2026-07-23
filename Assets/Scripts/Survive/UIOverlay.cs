using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class UIOverlay : MonoBehaviour
{
    private Label currentDayLabel;
    private Label currentTimeLabel;
    private Label hungerLabel;
    private Label fatigueLabel;
    private Label painLabel;
    private Label sanityLabel;

    void Awake()
    {
        currentDayLabel = GetComponent<UIDocument>().rootVisualElement.Q<Label>("DayLabel");
        currentTimeLabel = GetComponent<UIDocument>().rootVisualElement.Q<Label>("TimeLabel");
        hungerLabel = GetComponent<UIDocument>().rootVisualElement.Q<Label>("HungerLabel");
        fatigueLabel = GetComponent<UIDocument>().rootVisualElement.Q<Label>("FatigueLabel");
        painLabel = GetComponent<UIDocument>().rootVisualElement.Q<Label>("PainLabel");
        sanityLabel = GetComponent<UIDocument>().rootVisualElement.Q<Label>("SanityLabel");
    }

    public void UpdateInfo()
    {
        if (DayMonthManager.Instance == null || StatManager.Instance == null) return;
        if (currentDayLabel == null || currentTimeLabel == null || hungerLabel == null || fatigueLabel == null || painLabel == null || sanityLabel == null) return;
        currentDayLabel.text = $"Day {DayMonthManager.Instance.currentDay}: ";
        currentTimeLabel.text = $"{DayMonthManager.Instance.currentTimeUI}";
        hungerLabel.style.color = StatManager.Instance.stats["hunger"].currentValue > 80 ? Color.red : StatManager.Instance.stats["hunger"].currentValue > 40 ? Color.yellow : Color.green;
        fatigueLabel.style.color = StatManager.Instance.stats["fatigue"].currentValue > 80 ? Color.red : StatManager.Instance.stats["fatigue"].currentValue > 40 ? Color.yellow : Color.green;
        painLabel.style.color = StatManager.Instance.stats["pain"].currentValue > 80 ? Color.red : StatManager.Instance.stats["pain"].currentValue > 40 ? Color.yellow : Color.green;
        sanityLabel.style.color = StatManager.Instance.stats["sanity"].currentValue < 20 ? Color.red : StatManager.Instance.stats["sanity"].currentValue < 40 ? Color.yellow : Color.green;
    }
    public void LateUpdate()
    {
        UpdateInfo();
    }
}
