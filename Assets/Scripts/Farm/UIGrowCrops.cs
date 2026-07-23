using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIGrowCrops : MonoBehaviour
{
    private Button plantButton;
    private Button waterButton;
    private Button harvestButton;

    private Label plantDescriptionText;
    private Label waterDescriptionText;
    private Label harvestDescriptionText;

    private VisualElement panel;
    [SerializeField] private UIDocument document;

    private IField currentField;
    private FarmPlotState currentFarmPlotState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plantButton = document.rootVisualElement.Q<Button>("PlantButton");
        plantButton.clicked += OnPlantClicked;

        waterButton = document.rootVisualElement.Q<Button>("WaterButton");
        waterButton.clicked += OnWaterClicked;

        harvestButton = document.rootVisualElement.Q<Button>("HarvestButton");
        harvestButton.clicked += OnHarvestClicked;

        panel = document.rootVisualElement.Q<VisualElement>("MainPanel");

        // add description text to the panel
        plantDescriptionText = document.rootVisualElement.Q<Label>("PlantDescription");
        waterDescriptionText = document.rootVisualElement.Q<Label>("WaterDescription");
        harvestDescriptionText = document.rootVisualElement.Q<Label>("HarvestDescription");

        // Hide the panel initially
        HidePanel();
    }
    private void OnPlantClicked()
    {
        // another ui to choose seed to plant, then call the Plant method in IField
        currentField?.Plant();
        HidePanel();
    }
    private void OnWaterClicked()
    {
        currentField?.Water();
        HidePanel();
    }
    private void OnHarvestClicked()
    {
        currentField?.Harvest();
        HidePanel();
    }
    public void ShowPanel(IField field)
    {
        if (currentFarmPlotState != null) 
        { 
            currentFarmPlotState.OnStateChanged -= UpdateButtonStates;
        }
        currentField = field;
        currentFarmPlotState = field.farmPlotState;
        currentFarmPlotState.OnStateChanged += UpdateButtonStates;

        //sync button states with current state of the field
        UpdateButtonStates(currentFarmPlotState.CurrentState);
        panel.style.visibility = Visibility.Visible;
    }
    public void HidePanel()
    {
        if (currentFarmPlotState != null)
        {
            currentFarmPlotState.OnStateChanged -= UpdateButtonStates;
            currentFarmPlotState = null;
        }
        currentField = null;
        currentFarmPlotState = null;
        panel.style.visibility = Visibility.Hidden;
    }
    void UpdateButtonStates(State currentState)
    {
        if (currentState == currentFarmPlotState.EmptyStateInstance)
        {
            plantButton.SetEnabled(true);
            waterButton.SetEnabled(false);
            harvestButton.SetEnabled(false);
            plantDescriptionText.text = "(1:00)";
            waterDescriptionText.text = "";
            harvestDescriptionText.text = "";
        }
        else if (currentState == currentFarmPlotState.GrowingStateInstance)
        {
            plantButton.SetEnabled(false);
            waterButton.SetEnabled(true);
            harvestButton.SetEnabled(true);
            plantDescriptionText.text = "";
            waterDescriptionText.text = "(1:00)";
            harvestDescriptionText.text = "(1:00)";
        }
        else if (currentState == currentFarmPlotState.HarvestStateInstance)
        {
            plantButton.SetEnabled(false);
            waterButton.SetEnabled(true);
            harvestButton.SetEnabled(true);
            plantDescriptionText.text = "";
            waterDescriptionText.text = "(1:00)";
            harvestDescriptionText.text = "(1:00) ++Product";
        }
        else if (currentState == currentFarmPlotState.WitheringStateInstance)
        {
            plantButton.SetEnabled(false);
            waterButton.SetEnabled(true);
            harvestButton.SetEnabled(true);
            plantDescriptionText.text = "";
            waterDescriptionText.text = "(1:00)";
            harvestDescriptionText.text = "(1:00) +Product +Seed";
        }
        else if (currentState == currentFarmPlotState.DeadStateInstance)
        {
            plantButton.SetEnabled(false);
            waterButton.SetEnabled(false);
            harvestButton.SetEnabled(true);
            plantDescriptionText.text = "";
            waterDescriptionText.text = "";
            harvestDescriptionText.text = "(1:00) ++Seed";
        }
        else
        {
            plantButton.SetEnabled(false);
            waterButton.SetEnabled(false);
            harvestButton.SetEnabled(false);
            plantDescriptionText.text = "Unknown state.";
            waterDescriptionText.text = "Unknown state.";
            harvestDescriptionText.text = "Unknown state.";
        }
    }
}
