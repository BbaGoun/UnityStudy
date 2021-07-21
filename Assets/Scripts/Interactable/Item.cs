using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class Item : ScriptableObject
{
    public string _name = "Item";
    public Sprite icon = null;
    public AudioClip audioClip = null;

    public virtual void Use()
    {
        Debug.Log("Using " + _name);
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }
}
