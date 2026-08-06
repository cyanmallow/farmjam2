using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UISettingsMainMenu : MonoBehaviour
{
    public static UISettingsMainMenu Instance { get; private set; }
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
        Debug.Log(backButton);

        //gameObject.SetActive(false);
    }

    private void OnBackClicked()
    {
        //gameObject.SetActive(false);
        UIMainMenu.Instance.gameObject.SetActive(true);
    }

}
