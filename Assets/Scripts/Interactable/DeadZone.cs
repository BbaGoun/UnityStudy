using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : InArea
{
    public override void OnInArea()
    {
        Player.Instance.combat.Die();
    }
}
