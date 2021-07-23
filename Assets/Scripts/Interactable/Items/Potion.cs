using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Inventory/Potion", order = 0)]
public class Potion : Item
{
    public int heal;

    public override void Use()
    {
        Player.Instance.stat.Heal(heal);
        RemoveFromInventory();
    }

}
