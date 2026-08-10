using UnityEngine;
using UnityEngine.UIElements;

public class UISettingsInGameMenu : MonoBehaviour
{
    public static UISettingsInGameMenu Instance { get; private set; }
    private UIDocument document;
    private Button backButton;
    private Button saveButton; 
    private Button loadButton;
    public VisualElement settingsVisualElement;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        document = GetComponent<UIDocument>();
        settingsVisualElement = document.rootVisualElement.Q<VisualElement>("SettingsVisualElement");
        backButton = document.rootVisualElement.Q<Button>("BackButton");
        backButton.clicked += OnBackClicked;

        saveButton = document.rootVisualElement.Q<Button>("SaveButton");
        saveButton.clicked += OnSaveClicked;

        loadButton = document.rootVisualElement.Q<Button>("LoadButton");
        loadButton.clicked += OnLoadClicked;

        settingsVisualElement.style.visibility = Visibility.Hidden;
    }
    private void OnLoadClicked()
    {
        SaveSystem.LoadGame();
    }

    private void OnSaveClicked()
    {
        SaveSystem.SaveGame();
    }

    private void OnBackClicked()
    {
        //gameObject.SetActive(false);
        settingsVisualElement.style.visibility = Visibility.Hidden;
    }

}
