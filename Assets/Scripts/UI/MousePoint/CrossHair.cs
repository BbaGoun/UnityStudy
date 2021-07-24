using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask layerMask;
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;
        if (Physics.Raycast(ray, out hitResult, 100f))
        {
            transform.position = new Vector3(hitResult.point.x, hitResult.point.y, hitResult.point.z);
        }
    }
}
