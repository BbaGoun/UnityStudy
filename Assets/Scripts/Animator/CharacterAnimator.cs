using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    // 캐릭터들의 애니메이터 파라미터를 조절하는 부분.

    Animator animator;
    CharacterCombat combat;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();
    }

    private void OnEnable()
    {
        combat.OnIdle += OnIdle;
        combat.OnAttack += OnSwing;
        combat.OnHitted += OnHitted;
        combat.OnDie += OnDie;
    }

    private void Update()
    {
        animator.SetBool("Walk", true);
    }

    void OnIdle()
    {
        animator.SetBool("Walk", false);
    }

    void OnSwing()
    {
        animator.SetTrigger("Swing");
    }

    void OnHitted()
    {
        animator.SetTrigger("Hitted");
    }

    void OnDie()
    {
        animator.SetTrigger("Die");
    }

    private void OnDisable()
    {
        combat.OnIdle -= OnIdle;
        combat.OnAttack -= OnSwing;
        combat.OnHitted -= OnHitted;
        combat.OnDie -= OnDie;
    }
}
