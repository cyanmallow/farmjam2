using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class NotifyUILogic : MonoBehaviour
{
    public static NotifyUILogic Instance { get; private set; }
    [SerializeField] private UIDocument UIdocument;
    private Label notificationText;
    private VisualElement UIContainer;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UIContainer = UIdocument.rootVisualElement.Q<VisualElement>("MainPanel");
        notificationText = UIdocument.rootVisualElement.Q<Label>("NotificationLabel");
        UIContainer.style.display = DisplayStyle.None;
    }

    public void DisplayNotification(string message)
    {
        UIContainer.style.display = DisplayStyle.Flex;
        notificationText.text = message;
        StartCoroutine(ThreeSecondsDisappear());
    }
    private IEnumerator ThreeSecondsDisappear()
    {
        // Wait for exactly 3 seconds
        yield return new WaitForSeconds(3f);
        UIContainer.style.display = DisplayStyle.None;
    }
}
