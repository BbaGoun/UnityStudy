using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public int currentHP;
    public int maxHP;

    public int minPower = 7;
    public int maxPower = 13;

    private void OnEnable()
    {
        Respawn();
    }

    public void Respawn()
    {
        currentHP = maxHP;
    }

    public void Heal(int heal)
    {
        currentHP += heal;

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

#if UNITY_EDITOR
        Debug.Log("HP Healed : " + currentHP);
#endif
    }

    public bool GetDamage(int damage)
    {
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHP -= damage;
#if UNITY_EDITOR
        Debug.LogFormat("{0}'s HP : {1}", transform.name, currentHP);
#endif
        if (currentHP <= 0)
        {
            return true;
        }
        return false;
    }
}
