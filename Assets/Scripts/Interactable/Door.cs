using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    bool isClosed = true;
    Transform a;
    Vector3 closedPosition = new Vector3(5.3f, 0, 0);
    Quaternion closedRotation = new Quaternion(0f, 180f, 0f, 0f);
    Vector3 openedPosition = new Vector3(4.2f, 0f, 1f);
    Quaternion openedRotation = new Quaternion(0f, 90f, 0f, 0f);

    public float delay = 1f;

    public override void Interact()
    {
        if(!isInteracted)
        {
            isInteracted = true;
            if(isClosed)
            {
#if UNITY_EDITOR
                Debug.Log("Door Open");
#endif
                OpenDoor();
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("Door Close");
#endif
                CloseDoor();
            }
            StartCoroutine(GiveInteractDelay(delay));
        }
    }

    void OpenDoor()
    {
        isClosed = false;
        transform.localPosition = openedPosition;
        transform.Rotate(0f, 90f, 0f, Space.Self);
    }

    void CloseDoor()    
    {
        isClosed = true;
        transform.localPosition = closedPosition;
        transform.Rotate(0f, -90f, 0f, Space.Self);
    }
}
