using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    InventorySlot[] slots;
    public Transform slotParent;

    public GameObject inventoryHead;

    private void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            InventoryToggle();
        }
    }

    private void OnEnable()
    {
        inventory = Inventory.Instance;
        inventory.onItemChanged += UpdateUI;

        slots = slotParent.GetComponentsInChildren<InventorySlot>();
    }

    public void InventoryToggle()
    {
        inventoryHead.SetActive(!inventoryHead.activeSelf);
    }

    void UpdateUI()
    {
        int slotsSize = slots.Length;
        for (int i = 0; i < slotsSize; i++)
        {
            if(i<inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot(); 
            }
        }
    }
}
