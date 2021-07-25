using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    Vector3 offset;
    public float camSpeed = 0.1f;

    private void Start()
    {
        offset = playerTransform.position - transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position - offset, camSpeed);
    }
}
