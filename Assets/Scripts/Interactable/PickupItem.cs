using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable
{
    public Item item;
    public override void Interact()
    {
        SelectItem();
    }

    void SelectItem()
    {

        bool isPickuped = Inventory.Instance.Add(item);
        if (isPickuped)
        {
            if (item.audioClip != null)
            {
                AudioManager.Instance.source.clip = item.audioClip;
                AudioManager.Instance.source.Play();
            }
            Destroy(this.gameObject);
        }
    }
}
