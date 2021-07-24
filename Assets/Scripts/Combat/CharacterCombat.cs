using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterCombat : MonoBehaviour
{
    // CharacterAnimator에서 함수 추가.
    public event Action OnAttack;
    public event Action OnHitted;
    public event Action OnDie;
    // Enemy에서 함수 추가.
    public event Action OnHPZero;

    public enum Character { PLAYER, ENEMY, ELSE };
    public Character character;

    const float cooltime = 2f;
    public float attackCooltime = 0f;

    CharacterStat myStat;
    public Transform hPBarTf;
    public Collider hitBox;

    private void Awake()
    {
        myStat = GetComponent<CharacterStat>();
        HPBarManager.Instance.Create(hPBarTf, myStat);
    }

    public void Attack()
    {
        if (attackCooltime <= 0f)
        {
            OnAttack?.Invoke();
            StartCoroutine(OnHitBox(0.25f, 0.6f));

            attackCooltime = cooltime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterCombat enemyCombat = other.gameObject.GetComponent<CharacterCombat>();
        if (enemyCombat != null)
            StartCoroutine(GiveDamage(enemyCombat, 0.5f));
    }

    IEnumerator OnHitBox(float predelay, float duration)
    {
        yield return new WaitForSeconds(predelay);
        hitBox.enabled = true;
        yield return new WaitForSeconds(duration);
        hitBox.enabled = false;
    }

    IEnumerator GiveDamage(CharacterCombat enemyCombat, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (enemyCombat != null)
            enemyCombat.Hitted(UnityEngine.Random.Range(myStat.minPower, myStat.maxPower));
    }

    public void Hitted(int damage)
    {
        OnHitted?.Invoke();
        bool isDead = myStat.GetDamage(damage);
        if (isDead)
        {
            Die();
        }
    }

    public void Die()
    {
        switch (character)
        {
            case Character.PLAYER:
                DisablePlayerController();
                break;
            case Character.ENEMY:
                DisableEnemyController();
                break;
            case Character.ELSE:
                break;
        }
        OnHPZero?.Invoke();
        OnDie?.Invoke();
    }

    public void DisablePlayerController()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
            playerController.enabled = false;
    }

    public void DisableEnemyController()
    {
        EnemyController enemyController = GetComponent<EnemyController>();
        if (enemyController != null)
            enemyController.enabled = false;
    }

    private void Update()
    {
        attackCooltime -= Time.deltaTime;
    }
}
