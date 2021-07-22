using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    public bool isInteracted = false;
    public float delay = 1.0f;
    Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = new Color32(255, 101, 0, 255);
        outline.OutlineWidth = 8f;
        outline.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        Player.Instance.controller.nearInteractable = this;
        outline.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Player.Instance.controller.nearInteractable = null;
        outline.enabled = false;
    }

    public virtual void interact()
    {

    }

    public void Interact()
    {
        if (!isInteracted)
        {
            isInteracted = true;
            interact();
            StartCoroutine(GiveInteractDelay(delay));
        }
    }

    public IEnumerator GiveInteractDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isInteracted = false;
    }
}
