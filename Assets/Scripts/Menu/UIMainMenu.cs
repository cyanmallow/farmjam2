using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIMainMenu : MonoBehaviour
{
    public static UIMainMenu Instance { get; private set;  } 
    private UIDocument document;
    private Button newGameButton;
    private Button loadGameButton;
    private Button optionsButton;
    private Button quitButton;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        document = GetComponent<UIDocument>();
        newGameButton = document.rootVisualElement.Q<Button>("NewGameButton");
        newGameButton.clicked += OnNewGameClicked;

        loadGameButton = document.rootVisualElement.Q<Button>("LoadGameButton");
        loadGameButton.clicked += OnLoadGameClicked;

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
        //UISettingMenu.Instance.gameObject.SetActive(true);
    }

    private void OnNewGameClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnLoadGameClicked()
    {
        // if save data exists
        if (Application.persistentDataPath + "/gamesave.json" != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SaveSystem.LoadGame();
        }
        else
        {
            Debug.Log("No save data");
        }
    }
}
