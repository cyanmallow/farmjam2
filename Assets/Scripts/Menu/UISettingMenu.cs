using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UISettingMenu : MonoBehaviour
{
    public static UISettingMenu Instance { get; private set; }
    private UIDocument document;
    private Button backButton;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        document = GetComponent<UIDocument>();
        backButton = document.rootVisualElement.Q<Button>("BackButton");
        backButton.clicked += OnBackClicked;
    }

    private void OnBackClicked()
    {
        gameObject.SetActive(false);
        UIMainMenu.Instance.gameObject.SetActive(true);
    }

}
