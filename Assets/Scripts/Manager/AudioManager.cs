using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public AudioSource source;
    private void Awake()
    {
        Instance = this;
    }
}
