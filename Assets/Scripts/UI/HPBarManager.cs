using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarManager : MonoBehaviour
{
    public HPBar hPBar;
    private static HPBarManager instance;
    public static HPBarManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(HPBarManager)) as HPBarManager;

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Create(Transform target, CharacterStat stat)
    {
        HPBar newHPBar = Instantiate(hPBar, transform) as HPBar;
        newHPBar.Init(target, stat);
    }
}
