using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(Player)) as Player;

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    public CharacterStat stat;
    public CharacterCombat combat;

    private void Awake()
    {
        Instance = this;
        stat = GetComponent<CharacterStat>();
        combat = GetComponent<CharacterCombat>();
    }

    private void OnEnable()
    {
        combat.OnHPZero += Die;
    }

    private void OnDisable()
    {
        combat.OnHPZero -= Die;
    }

    void Die()
    {
        Debug.Log("Player is dead");
    }
}
