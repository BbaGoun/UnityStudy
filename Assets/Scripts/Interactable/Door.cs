using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    bool isClosed = true;
    Vector3 closedPosition = new Vector3(5.3f, 0, 0);
    Vector3 openedPosition = new Vector3(4.2f, 0f, 1f);
    public AudioClip fx;

    AudioSource source;

    private void OnEnable()
    {
        source = AudioManager.Instance.source;
    }

    public override void interact()
    {
        ToggleDoor();
        if(fx != null)
        {
            source.clip = fx;
            source.Play();
        }
    }

    void ToggleDoor()
    {
        if (isClosed)
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
