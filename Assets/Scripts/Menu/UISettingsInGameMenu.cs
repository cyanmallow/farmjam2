using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UISettingsInGameMenu : MonoBehaviour
{
    public static UISettingsInGameMenu Instance { get; private set; }
    private UIDocument document;
    private Button backButton;
    private Button saveButton; 
    private Button loadButton;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        document = GetComponent<UIDocument>();
        backButton = document.rootVisualElement.Q<Button>("BackButton");
        backButton.clicked += OnBackClicked;

        saveButton = document.rootVisualElement.Q<Button>("SaveButton");
        saveButton.clicked += OnSaveClicked;

        loadButton = document.rootVisualElement.Q<Button>("LoadButton");
        loadButton.clicked += OnLoadClicked;
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
        gameObject.SetActive(false);
    }

}
