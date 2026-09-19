using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("インベントリ本体")]
    public GameObject inventoryPanel;

    [Header("スロットを並べる場所")]
    public Transform slotParent;

    [Header("スロットPrefab")]
    public GameObject slotPrefab;

    [Header("スロット数")]
    public int slotCount = 16;

    private List<InventorySlot> slots =
        new List<InventorySlot>();

    void Start()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }

        CreateSlots();
    }

    void CreateSlots()
    {
        if (slotParent == null)
        {
            Debug.LogWarning(
                "Slot Parentが設定されていません！"
            );
            return;
        }

        if (slotPrefab == null)
        {
            Debug.LogWarning(
                "Slot Prefabが設定されていません！"
            );
            return;
        }

        slots.Clear();

        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObject =
                Instantiate(
                    slotPrefab,
                    slotParent
                );

            InventorySlot slot =
                slotObject.GetComponent<InventorySlot>();

            if (slot != null)
            {
                slots.Add(slot);

                slot.SetSlot(
                    "",
                    0,
                    null
                );
            }
        }
    }

    public void ToggleInventory()
    {
        if (inventoryPanel == null)
        {
            Debug.LogWarning(
                "Inventory Panelが設定されていません！"
            );
            return;
        }

        bool isOpen =
            inventoryPanel.activeSelf;

        inventoryPanel.SetActive(!isOpen);
    }

    public void UpdateInventory(
        Dictionary<string, InventoryElement> inventory
    )
    {
        if (slots.Count == 0)
        {
            Debug.LogWarning(
                "インベントリスロットがありません！"
            );
            return;
        }

        // 全スロットを空にする
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetSlot(
                "",
                0,
                null
            );
        }

        int slotIndex = 0;

        foreach (
            KeyValuePair<string, InventoryElement> item
            in inventory
        )
        {
            if (slotIndex >= slots.Count)
            {
                Debug.LogWarning(
                    "インベントリがいっぱいです！"
                );
                break;
            }

            InventoryElement element =
                item.Value;

            slots[slotIndex].SetSlot(
                element.displayName,
                element.amount,
                element.icon
            );

            slotIndex++;
        }
    }
}