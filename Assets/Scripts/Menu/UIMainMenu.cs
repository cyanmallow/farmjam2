using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIMainMenu : MonoBehaviour
{
    public static UIMainMenu Instance { get; private set;  } 
    private UIDocument document;
    private Button playButton;
    private Button optionsButton;
    private Button quitButton;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        document = GetComponent<UIDocument>();
        playButton = document.rootVisualElement.Q<Button>("PlayButton");
        playButton.clicked += OnPlayClicked;

        optionsButton = document.rootVisualElement.Q<Button>("OptionsButton");
        optionsButton.clicked += OnOptionsClicked;

        quitButton = document.rootVisualElement.Q<Button>("QuitButton");
        quitButton.clicked += OnQuitClicked;
    }

    private void OnQuitClicked()
    {
        throw new NotImplementedException();
    }

    private void OnOptionsClicked()
    {
        gameObject.SetActive(false);
        UISettingMenu.Instance.gameObject.SetActive(true);
    }

    private void OnPlayClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
