using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBuildings : InField
{
    public MeshRenderer ceiling;
    public override void OnInField()
    {
        ceiling.enabled = false;
    }

    public override void OnOutField()
    {
        ceiling.enabled = true;
    }
}
