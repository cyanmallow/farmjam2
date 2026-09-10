using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// HOW TO USE:
// Create a UI Document with a VisualElement named "MainPanel" and another VisualElement named "OptionsContainer" inside it.
// Could also copy the 'OptionsUI' from farmjam2
// declare a var with all the options in it, see below
// call GiveOptions() + add Actions as methods
// example below: Test2options

public class PopulateButtons : MonoBehaviour
{
    [SerializeField] private UIDocument UIdocument;
    private VisualElement UIContainer;
    private VisualElement optionsContainer;
    private Button cancelButton;

    private Action<Option> onOptionSelectedCallback;

    void Start()
    {
        UIContainer = UIdocument.rootVisualElement.Q<VisualElement>("MainPanel");
        optionsContainer = UIdocument.rootVisualElement.Q<VisualElement>("OptionsContainer");
        cancelButton = UIdocument.rootVisualElement.Q<Button>("CancelButton");
        cancelButton.clicked += CancelButtonClicked;
        HidePanel();

        // Replace this
        Test2options();
    }
    // Replace this
    public void Test2options()
    {
        var options = new List<Option> {
            new Option {optionName = "Option 1", action = PrintChoose1 },
            new Option {optionName = "Option 2", action = PrintChoose2 }
        };

        GiveOptions(options, (chosen) => {
            chosen.action?.Invoke();
        });
    }
    private void PrintChoose1()
    {
        Debug.Log("Option 1 chosen");
    }
    private void PrintChoose2()
    {
        Debug.Log("Option 2 chosen");
    }
    // End replace

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
            // Create a button
            var optionButton = new Button(() => OnOptionSelected(option))
            {
                text = option.optionName
            };
            optionButton.AddToClassList("seed-button"); // used seed button's styling to test
            optionsContainer.Add(optionButton);
        }
    }

    // Tell the button what to do if clicked
    private void OnOptionSelected(Option option)
    {
        HidePanel();
        onOptionSelectedCallback?.Invoke(option);
        onOptionSelectedCallback = null; // avoid a stale callback firing next time
    }

    public void ShowPanel()
    {
        UIContainer.style.display = DisplayStyle.Flex;
    }
    public void HidePanel()
    {
        UIContainer.style.display = DisplayStyle.None;
    }
    public void CancelButtonClicked()
    {
        HidePanel();
        onOptionSelectedCallback = null; // avoid a stale callback firing next time
    }
}