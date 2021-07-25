using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable
{
    public Item item;
    public override void interact()
    {
        SelectItem();
    }

    void SelectItem()
    {
        bool isPickuped = Inventory.Instance.Add(item);
        if (isPickuped)
        {
            if (item.pickupSound != null)
            {
                AudioManager.Instance.source.clip = item.pickupSound;
                AudioManager.Instance.source.Play();
            }
            Destroy(this.gameObject);
        }
    }
}
