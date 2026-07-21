using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    public UnityEngine.UIElements.Image iconImage;
    public UnityEngine.UIElements.TextField quantityText;
    private UIDocument UIdocument;

    private void Awake()
    {
        UIdocument = GetComponent<UIDocument>();
        iconImage = UIdocument.rootVisualElement.Q<UnityEngine.UIElements.Image>("IconImage");
        quantityText = UIdocument.rootVisualElement.Q<UnityEngine.UIElements.TextField>("QuantityText");
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
