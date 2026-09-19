using UnityEngine;

public class ElementItem : MonoBehaviour
{
    [Header("元素")]
    public string elementName = "Carbon";

    [Header("表示名")]
    public string displayName = "炭素";

    [Header("アイコン")]
    public Sprite icon;

    [Header("入手個数")]
    public int amount = 1;

    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (collected)
        {
            return;
        }

        collected = true;

        InventoryManager inventory =
            FindFirstObjectByType<InventoryManager>();

        if (inventory != null)
        {
            inventory.AddElement(
                elementName,
                displayName,
                icon,
                amount
            );

            Debug.Log(
                displayName + "を入手！"
            );
        }
        else
        {
            Debug.LogWarning(
                "InventoryManagerが見つかりません！"
            );
        }

        Destroy(gameObject);
    }
}