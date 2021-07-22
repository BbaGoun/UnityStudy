using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public Image icon;
    public Button xButton;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
        if(newItem)
        xButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.enabled = false;
        xButton.interactable = false;
    }

    public void OnXButtonClicked()
    {
        Inventory.Instance.Remove(item);
    }

    public void OnItemButtonClicked()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
