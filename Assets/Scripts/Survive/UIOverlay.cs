using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
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
    private VisualElement inventoryUI;
    private VisualElement container;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        document = GetComponent<UIDocument>();
        currentDayLabel = document.rootVisualElement.Q<Label>("DayLabel");
        currentTimeLabel = document.rootVisualElement.Q<Label>("TimeLabel");
        hungerLabel = document.rootVisualElement.Q<Label>("HungerLabel");
        fatigueLabel = document.rootVisualElement.Q<Label>("FatigueLabel");
        painLabel = document.rootVisualElement.Q<Label>("PainLabel");
        sanityLabel = document.rootVisualElement.Q<Label>("SanityLabel");

        container = document.rootVisualElement.Q<VisualElement>("Container");
        container.pickingMode = PickingMode.Ignore;

        settingsButton = document.rootVisualElement.Q<Button>("SettingsButton");
        settingsButton.clicked += OnSettingsClicked;

        inventoryUI = document.rootVisualElement.Q<VisualElement>("InventoryVE");
        inventoryUI.RegisterCallback<ClickEvent>(evt =>
        {
            UIInventory.Instance.ToggleInventoryUI();
            ToggleOverlayUI();
        });
        UpdateInfo();
    }
    public void ToggleOverlayUI()
    {
        if (container.style.visibility == Visibility.Hidden)
        {
            container.style.visibility = Visibility.Visible;
        }
        else
        {
            container.style.visibility = Visibility.Hidden;
        }
    }
    private void OnSettingsClicked()
    {
        UISettingsInGameMenu.Instance.settingsVisualElement.style.visibility = Visibility.Visible;
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
