using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    CharacterCombat combat;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();
    }

    private void OnEnable()
    {
        combat.OnAttack += Swing;
        combat.OnHitted += Hitted;
        combat.OnDie += Die;
    }

    public void Idle()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Walk", false);
    }

    public void Walk()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Walk", true);
    }

    public void Run()
    {
        anim.SetBool("Run", true);
        anim.SetBool("Walk", false);
    }

    public void Jump()
    {
        anim.SetTrigger("Jump");
    }

    public void Swing()
    {
        anim.SetTrigger("Swing");
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
