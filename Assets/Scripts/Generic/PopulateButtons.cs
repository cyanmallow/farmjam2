using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PopulateButtons : MonoBehaviour
{
    public static PopulateButtons Instance { get; private set; }

    [SerializeField] private UIDocument UIdocument;
    //private VisualElement UIContainer;
    private VisualElement wholeContainer;

    private VisualElement optionsContainer;
    private Button outsideButton;

    private Button cancelButton;

    private Action<Option> onOptionSelectedCallback;
    void Awake()
    {
        wholeContainer = UIdocument.rootVisualElement.Q<VisualElement>("WholePanel");
        //UIContainer = UIdocument.rootVisualElement.Q<VisualElement>("MainPanel");
        optionsContainer = UIdocument.rootVisualElement.Q<VisualElement>("OptionsContainer");
        outsideButton = UIdocument.rootVisualElement.Q<Button>("OutsideButton");
        outsideButton.clicked += CancelButtonClicked;
        cancelButton = UIdocument.rootVisualElement.Q<Button>("CancelButton");
        cancelButton.clicked += CancelButtonClicked;
        HidePanel();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    // Store callback (onSelected) as onOptionSelectedCallback 
    public void GiveOptions(List<Option> optionsList, Action<Option> onSelected)
    {
        onOptionSelectedCallback = onSelected; 
        HidePanel();
        ShowPanel();
        PopulateButton(optionsList);
    }

    public void PopulateButton(List<Option> optionsList)
    {
        // Clear existing buttons
        optionsContainer.Clear();
        foreach (var option in optionsList)
        {
            var optionButton = new Button();
            optionButton.AddToClassList("seed-button");

            var nameLabel = new Label(option.optionName);
            nameLabel.pickingMode = PickingMode.Ignore;

            var timeLabel = new Label($"{option.optionTime}h");
            timeLabel.pickingMode = PickingMode.Ignore;
            timeLabel.AddToClassList("time-button");

            optionButton.Add(nameLabel);
            optionButton.Add(timeLabel);

            optionButton.clicked += () =>
            {
                // disabled options doesnt fire when clicked, so we cant show the reason in pop-up
                // so dont use optionButton.SetEnabled(false);
                if (!option.isOptionAvailable)
                {
                    NotifyUILogic.Instance.DisplayNotification(option.unavailableReason);
                    return;
                }
                OnOptionSelected(option);
            };

            if (!option.isOptionAvailable)
                optionButton.AddToClassList("option-unavailable");

            optionsContainer.Add(optionButton);
        }
    }

    // Tell the button what to do if clicked
    private void OnOptionSelected(Option option)
    {
        var cb = onOptionSelectedCallback;
        onOptionSelectedCallback = null; // avoid a stale callback firing next time
        HidePanel();
        // calls the callback function that was passed in when GiveOptions was called, passing the selected option as an argument. This allows the caller to handle the selected option appropriately.
        cb?.Invoke(option);
    }

    public void ShowPanel()
    {
        wholeContainer.style.display = DisplayStyle.Flex;
    }
    public void HidePanel()
    {
        wholeContainer.style.display = DisplayStyle.None;
    }
    public void CancelButtonClicked()
    {
        Debug.Log("Cancel button clicked");
        HidePanel();
        onOptionSelectedCallback = null; // avoid a stale callback firing next time
    }
}