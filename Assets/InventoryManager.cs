using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InventoryElement
{
    public string elementName;
    public string displayName;
    public int amount;
    public Sprite icon;
}

public class InventoryManager : MonoBehaviour
{
    private Dictionary<string, InventoryElement> inventory =
        new Dictionary<string, InventoryElement>();

    [Header("インベントリUI")]
    public InventoryUI inventoryUI;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (inventoryUI != null)
            {
                inventoryUI.ToggleInventory();
            }
        }
    }

    // 元素を入手
    public void AddElement(
        string elementName,
        string displayName,
        Sprite icon,
        int amount = 1
    )
    {
        if (inventory.ContainsKey(elementName))
        {
            inventory[elementName].amount += amount;

            // 表示名とアイコンも最新のものに更新
            inventory[elementName].displayName = displayName;
            inventory[elementName].icon = icon;
        }
        else
        {
            InventoryElement newElement =
                new InventoryElement();

            newElement.elementName = elementName;
            newElement.displayName = displayName;
            newElement.amount = amount;
            newElement.icon = icon;

            inventory.Add(
                elementName,
                newElement
            );
        }

        Debug.Log(
            displayName +
            " を " +
            amount +
            " 個入手！"
        );

        UpdateUI();
    }

    // 元素を消費
    public bool RemoveElement(
        string elementName,
        int amount = 1
    )
    {
        if (!inventory.ContainsKey(elementName))
        {
            return false;
        }

        if (inventory[elementName].amount < amount)
        {
            return false;
        }

        inventory[elementName].amount -= amount;

        if (inventory[elementName].amount <= 0)
        {
            inventory.Remove(elementName);
        }

        UpdateUI();

        return true;
    }

    // 所持数を取得
    public int GetElementCount(
        string elementName
    )
    {
        if (inventory.ContainsKey(elementName))
        {
            return inventory[elementName].amount;
        }

        return 0;
    }

    // インベントリ取得
    public Dictionary<string, InventoryElement>
        GetInventory()
    {
        return inventory;
    }

    // UI更新
    void UpdateUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventory(
                inventory
            );
        }
    }
}