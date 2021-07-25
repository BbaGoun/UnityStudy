using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterCombat : MonoBehaviour
{
    // Animator에서 함수 추가.
    public event Action OnAttack;
    public event Action OnDie;
    // Animator and Controller
    public event Action OnHitted;

    public enum Character { PLAYER, ENEMY, ELSE };
    public Character character;

    const float attackCooltime = 2f;
    float lastAttackTime;

    public CharacterStat myStat;
    public Transform hPBarTf;
    public Collider hitBox;
    public GameObject Weapon;

    bool equipWeapon = false;

    public void EquipWeapon()
    {
        equipWeapon = true;
        Weapon.SetActive(true);
    }

    private void Awake()
    {
        HPBarManager.Instance.Create(hPBarTf, myStat);
    }

    public void Attack()
    {
        if (!equipWeapon)
            return;
        if (Time.time >= lastAttackTime + attackCooltime)
        {
            OnAttack?.Invoke();
            StartCoroutine(OnHitBox(0.25f, 0.6f));

            lastAttackTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterCombat enemyCombat = other.gameObject.GetComponent<CharacterCombat>();
        if (enemyCombat != null)
            GiveDamage(enemyCombat);
    }

    IEnumerator OnHitBox(float predelay, float duration)
    {
        yield return new WaitForSeconds(predelay);
        hitBox.enabled = true;
        yield return new WaitForSeconds(duration);
        hitBox.enabled = false;
    }

    void GiveDamage(CharacterCombat enemyCombat)
    {
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
#if UNITY_EDITOR
        Debug.Log("Player Die!");
#endif
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
}
