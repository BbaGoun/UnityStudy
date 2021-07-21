using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InField : MonoBehaviour
{
    public virtual void OnInField()
    {

    }

    public virtual void OnOutField()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
#if UNITY_EDITOR
        Debug.Log("Player In detected");
#endif
        OnInField();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
#if UNITY_EDITOR
        Debug.Log("Player Out detected");
#endif
        OnOutField();
    }
}
