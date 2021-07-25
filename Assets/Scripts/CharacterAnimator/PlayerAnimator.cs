using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    public CharacterCombat combat;
    public AudioClip jumpSound;
    public AudioClip swingSound;

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
        AudioManager.Instance.source.clip = jumpSound;
        AudioManager.Instance.source.Play();
    }

    public void Swing()
    {
        anim.SetTrigger("Swing");
        AudioManager.Instance.source.clip = swingSound;
        AudioManager.Instance.source.PlayDelayed(0.25f);
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
