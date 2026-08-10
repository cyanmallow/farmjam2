using System.Collections.Generic;
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
    [SerializeField] private UIDocument chooseSeedDocument;

    private IField currentField;
    private FarmPlotState currentFarmPlotState;

    private VisualElement seedSelectionPanel;
    private VisualElement seedButtonContainer;

    [SerializeField] private List<SeedData> allSeeds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panel = document.rootVisualElement.Q<VisualElement>("MainPanel");
        plantButton = document.rootVisualElement.Q<Button>("PlantButton");
        plantButton.clicked += OnPlantClicked;
        waterButton = document.rootVisualElement.Q<Button>("WaterButton");
        waterButton.clicked += OnWaterClicked;
        harvestButton = document.rootVisualElement.Q<Button>("HarvestButton");
        harvestButton.clicked += OnHarvestClicked;
        // add description text to the panel
        plantDescriptionText = document.rootVisualElement.Q<Label>("PlantDescription");
        waterDescriptionText = document.rootVisualElement.Q<Label>("WaterDescription");
        harvestDescriptionText = document.rootVisualElement.Q<Label>("HarvestDescription");

        // Initialize the seed selection panel and button container
        seedSelectionPanel = chooseSeedDocument.rootVisualElement.Q<VisualElement>("SeedSelectionPanel");
        seedButtonContainer = chooseSeedDocument.rootVisualElement.Q<VisualElement>("SeedButtonContainer");

        PopulateSeedButtons();
        HidePanel();
    }
    private void OnPlantClicked()
    {
        // another ui to choose seed to plant, then call the Plant method in IField
        //panel.style.visibility = Visibility.Hidden;
        seedSelectionPanel.style.visibility = Visibility.Visible;

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
    private void PopulateSeedButtons()
    {         
        // Clear existing buttons
        seedButtonContainer.Clear();
        // Get the list of seeds from the SeedManager
        foreach (var seed in allSeeds)
        {
            Debug.Log($"Seed name: '{seed.itemName}'");
            // Create a new button for each seed
            var seedButton = new Button(() => OnSeedSelected(seed))
            {
                text = seed.itemName
            };
            seedButton.AddToClassList("seed-button");
            // Add the button to the container
            seedButtonContainer.Add(seedButton);
        }
    }
    private void OnSeedSelected(SeedData seed)
    {
        currentField?.Plant(seed);
        HidePanel();
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
        seedSelectionPanel.style.visibility = Visibility.Hidden;
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
