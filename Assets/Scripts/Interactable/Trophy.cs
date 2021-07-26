using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : Interactable
{
    public AudioClip endingSong;
    public AudioSource fx;
    public AudioSource bgm;
    public GameObject message;

    private void OnEnable()
    {
        fx = AudioManager.Instance.source;
    }

    public override void interact()
    {
        bgm.Pause();
        if (endingSong != null)
        {
            fx.clip = endingSong;
            fx.Play();
        }
        message.SetActive(true);
    }
}
