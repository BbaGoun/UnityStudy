using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    CharacterCombat combat;

    private void OnEnable()
    {
        combat = GetComponent<CharacterCombat>();
        combat.OnHPZero += Die;
    }

    private void OnDisable()
    {
        combat.OnHPZero -= Die;
    }

    public override void interact()
    {
        //Player.Instance.combat.Attack(combat);
    }

    void Die()
    {
        Debug.Log("Enemy is dead");
        StartCoroutine(ReturnObject());
    }

    IEnumerator ReturnObject()
    {
        yield return new WaitForSeconds(2f);
        PoolingManager.Instance.ReturnObject(this.gameObject);
    }
}
