using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterCombat : MonoBehaviour
{
    // CharacterAnimator에서 함수 추가.
    public event Action OnIdle;
    public event Action OnAttack;
    public event Action OnHitted;
    public event Action OnDie;
    // Enemy에서 함수 추가.
    public event Action OnHPZero;

    public enum Character { PLAYER, ENEMY, ELSE };
    public Character character;

    const float cooltime = 3f;
    public float attackCooltime = 0f;
    float lastAttackTime;
    public bool isInCombat = false;
    // public float attackSpeed = 1f;
    // public float attackDelay = 1f;

    CharacterStat myStat;
    public Transform hPBarTf;

    private void Awake()
    {
        myStat = GetComponent<CharacterStat>();
        HPBarManager.Instance.Create(hPBarTf, myStat);
    }

    public void Attack(CharacterCombat enemyCombat)
    {
        if (attackCooltime <= 0f)
        {
            StartCoroutine(GiveDamage(enemyCombat, 0.5f));
            OnAttack?.Invoke();

            isInCombat = true;
            attackCooltime = cooltime;
            lastAttackTime = Time.time;
        }
    }

    IEnumerator GiveDamage(CharacterCombat enemyCombat, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (enemyCombat != null)
            enemyCombat.Hitted(myStat.power);
        else
            OnIdle?.Invoke();
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
            //case Character.ENEMY:
            //    DisableEnemyController();
                //break;
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

    //public void DisableEnemyController()
    //{
    //    EnemyController enemyController = GetComponent<EnemyController>();
    //    if (enemyController != null)
    //        enemyController.enabled = false;
    //}

    private void Update()
    {
        attackCooltime -= Time.deltaTime;

        if (Time.time - lastAttackTime > cooltime)
        {
            isInCombat = false;
        }
    }
}
