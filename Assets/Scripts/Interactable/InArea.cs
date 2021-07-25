using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InArea : MonoBehaviour
{
    public virtual void OnInArea()
    {

    }

    public virtual void OnOutArea()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
#if UNITY_EDITOR
        Debug.Log("Player In detected");
#endif
        OnInArea();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
#if UNITY_EDITOR
        Debug.Log("Player Out detected");
#endif
        OnOutArea();
    }
}
