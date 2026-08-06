using UnityEngine;
using UnityEngine.UIElements;

public class UIOverlay : MonoBehaviour
{
    public UIOverlay Instance {  get; private set; }
    private UIDocument document;
    private Label currentDayLabel;
    private Label currentTimeLabel;
    private Label hungerLabel;
    private Label fatigueLabel;
    private Label painLabel;
    private Label sanityLabel;
    private Button settingsButton;


    void Awake()
    {
        document = GetComponent<UIDocument>();
        currentDayLabel = document.rootVisualElement.Q<Label>("DayLabel");
        currentTimeLabel = document.rootVisualElement.Q<Label>("TimeLabel");
        hungerLabel = document.rootVisualElement.Q<Label>("HungerLabel");
        fatigueLabel = document.rootVisualElement.Q<Label>("FatigueLabel");
        painLabel = document.rootVisualElement.Q<Label>("PainLabel");
        sanityLabel = document.rootVisualElement.Q<Label>("SanityLabel");

        settingsButton = document.rootVisualElement.Q<Button>("SettingsButton");
        settingsButton.clicked += OnSettingsClicked;
        UpdateInfo();
    }

    private void OnSettingsClicked()
    {
        UISettingsInGameMenu.Instance.gameObject.SetActive(true);
    }

    public void UpdateInfo()
    {
        if (DayMonthManager.Instance == null || StatManager.Instance == null) return;
        if (currentDayLabel == null || currentTimeLabel == null || hungerLabel == null || fatigueLabel == null || painLabel == null || sanityLabel == null) return;
        currentDayLabel.text = $"Day {DayMonthManager.Instance.currentDay}: ";

        DayMonthManager.Instance.UpdateTimeUI();
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
