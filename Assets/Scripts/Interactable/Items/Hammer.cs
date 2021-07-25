using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hammer", menuName = "Inventory/Hammer", order = 0)]
public class Hammer : Item
{
    public override void Use()
    {
        Player.Instance.combat.EquipWeapon();
        if (useSound != null)
        {
            AudioManager.Instance.source.clip = useSound;
            AudioManager.Instance.source.Play();
        }
        RemoveFromInventory();
    }
}
