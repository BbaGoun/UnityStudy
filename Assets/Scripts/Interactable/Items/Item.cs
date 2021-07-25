using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class Item : ScriptableObject
{
    public string _name = "Item";
    public Sprite icon = null;
    public AudioClip pickupSound = null;
    public AudioClip useSound = null;

    public virtual void Use()
    {
#if UNITY_EDITOR
        Debug.Log("Using " + _name);
#endif
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }
}
