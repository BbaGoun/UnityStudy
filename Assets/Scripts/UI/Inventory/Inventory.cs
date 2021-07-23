using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;
    public static Inventory Instance {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(Inventory)) as Inventory;

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<Item> items = new List<Item>();
    public int maxSpace = 9;

    // InventoryUI에서 UpdateUI함수 추가.
    public delegate void OnItemChanged();
    public OnItemChanged onItemChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public bool Add(Item addedItem)
    {
        if(items.Count >= maxSpace)
        {
            return false;
        }
        items.Add(addedItem);
        onItemChanged?.Invoke();
        return true;
    }

    public void Remove(Item removedItem)
    {
        items.Remove(removedItem);
        onItemChanged?.Invoke();
    }

    public bool IsContain(Item item)
    {
        return items.Contains(item);
    }
}
