using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : Interactable
{
    public ParticleSystem pS;
    public Item neededItem;
    public GameObject treasure;

    public override void interact()
    {
        OpenChest();
    }

    void OpenChest()
    {
        bool keyItemExist = Inventory.Instance.IsContain(neededItem);
        if(keyItemExist)
        {
            Inventory.Instance.Remove(neededItem);
            pS.Play();
            treasure.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
