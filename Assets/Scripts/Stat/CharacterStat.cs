using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    int currentHP;
    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = value;
        }
    }
    public int maxHP;

    public int power = 10;

    private void OnEnable()
    {
        currentHP = maxHP;
    }

    private void Update()
    {

    }

    public void Heal(int heal)
    {
        currentHP += heal;

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        Debug.Log("HP Healed : " + currentHP);
    }

    public bool GetDamage(int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHP -= damage;
        Debug.LogFormat("{0}'s HP : {1}", transform.name, currentHP);
        if (currentHP <= 0)
        {
            return true;
        }
        return false;
    }
}
