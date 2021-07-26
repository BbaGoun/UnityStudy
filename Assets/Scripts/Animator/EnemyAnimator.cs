using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Animator anim;
    public CharacterCombat combat;
    public AudioClip attackSound;

    private void OnEnable()
    {
        combat.OnAttack += Attack;
        combat.OnHitted += Hitted;
        combat.OnDie += Die;
    }

    public void Idle()
    {
        anim.SetBool("Walk", false);
    }

    public void Walk()
    {
        anim.SetBool("Walk", true);
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
        EnemyAudioManager.Instance.source.clip = attackSound;
        EnemyAudioManager.Instance.source.PlayDelayed(0.25f);
    }

    public void Hitted()
    {
        anim.SetTrigger("Hitted");
    }

    public void Die()
    {
        anim.SetBool("Die", true);
    }
}
