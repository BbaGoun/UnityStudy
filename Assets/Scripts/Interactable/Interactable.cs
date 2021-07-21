using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isInteracted = false;

    public virtual void Interact()
    {

    }

    public IEnumerator GiveInteractDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isInteracted = false;
    }
}
