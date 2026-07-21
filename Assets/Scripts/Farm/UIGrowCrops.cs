using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UIGrowCrops : MonoBehaviour
{
    private Button plantButton;
    private Button waterButton;
    private Button harvestButton;
    private VisualElement panel;
    [SerializeField] private UIDocument document;
    private IField ifield;

    // make ui appear on field
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 worldOffset = new Vector3(0, 1f, 0);

    private void Awake()
    {
        ifield = GetComponentInParent<IField>();
    }
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

        // Hide the panel initially
        HidePanel();
    }
    private void OnPlantClicked()
    {
        // another ui to choose seed to plant, then call the Plant method in IField
        //    Plant(testRadish, currentTile);
        ifield.Plant();
        HidePanel();
    }
    private void OnWaterClicked()
    {
        ifield.Water();
        HidePanel();
    }
    private void OnHarvestClicked()
    {
        ifield.Harvest();
        HidePanel();
    }
    public void ShowPanel(Transform tileTransform)
    {
        panel.style.visibility = Visibility.Visible;
        Debug.Log("Panel shown!");
        Vector2 panelPos = RuntimePanelUtils.CameraTransformWorldToPanel(
            document.rootVisualElement.panel,
            tileTransform.position + worldOffset,
            cam
        );

        panel.style.left = panelPos.x - 25;
        panel.style.top = panelPos.y - 10;

    }
    public void HidePanel()
    {
        panel.style.visibility = Visibility.Hidden;
        Debug.Log("Panel hidden!");
    }
}
