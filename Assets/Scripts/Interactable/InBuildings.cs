using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBuildings : InArea
{
    public MeshRenderer ceiling;
    public override void OnInArea()
    {
        ceiling.enabled = false;
    }

    public override void OnOutArea()
    {
        ceiling.enabled = true;
    }
}
