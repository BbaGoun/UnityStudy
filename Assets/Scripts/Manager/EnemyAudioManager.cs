using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    private static EnemyAudioManager instance;
    public static EnemyAudioManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(EnemyAudioManager)) as EnemyAudioManager;

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
