using UnityEngine;
using UnityEngine.UIElements;

public class UIInventory : MonoBehaviour
{
    public static UIInventory Instance { get; private set; }
    public Image iconImage;
    public TextField quantityText;
    private UIDocument UIdocument;
    private VisualElement inventoryUI;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UIdocument = GetComponent<UIDocument>();
        iconImage = UIdocument.rootVisualElement.Q<Image>("IconImage");
        quantityText = UIdocument.rootVisualElement.Q<TextField>("QuantityText");
        inventoryUI = UIdocument.rootVisualElement.Q<VisualElement>("InventoryVisualElement");
        inventoryUI.style.visibility = Visibility.Hidden;
    }
    
    public void ToggleInventoryUI()
    {
        if (inventoryUI.style.visibility == Visibility.Hidden)
        {
            inventoryUI.style.visibility = Visibility.Visible;
        }
        else
        {
            inventoryUI.style.visibility = Visibility.Hidden;
        }

    }
    public void Refresh(InventorySlot slot)
    {
        if (slot.IsEmpty)
        {
            iconImage.SetEnabled(false);
            quantityText = null;
        }
        else
        {
            iconImage.SetEnabled(true);
            iconImage.sprite = slot.item.itemIcon;
            //quantityText = slot.quantityText;
        }
    }
}
