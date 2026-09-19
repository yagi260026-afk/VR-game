using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [Header("元素アイコン")]
    public Image icon;

    [Header("表示名")]
    public TextMeshProUGUI elementNameText;

    [Header("個数")]
    public TextMeshProUGUI amountText;

   public void SetSlot(
    string elementName,
    int amount,
    Sprite elementIcon
)
{
    Debug.Log(
        "SetSlotに渡された名前 = [" +
        elementName +
        "]"
    );

    if (elementNameText != null)
    {
        elementNameText.text = elementName;
    }

    if (amountText != null)
    {
        amountText.text = "× " + amount;
    }

    if (icon != null)
    {
        icon.sprite = elementIcon;

        icon.gameObject.SetActive(
            elementIcon != null
        );
    }
}
}