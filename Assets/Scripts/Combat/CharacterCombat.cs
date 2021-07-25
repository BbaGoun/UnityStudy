using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterCombat : MonoBehaviour
{
    // Animator에서 함수 추가.
    public event Action OnAttack;
    // Animator and Controller
    public event Action OnDie;
    public event Action OnHitted;

    public float attackCooltime = 2f;
    public float predelay;
    public float duration;
    float lastAttackTime;

    public CharacterStat myStat;
    public Transform hPBarTf;
    public Collider hitBox;
    public GameObject Weapon;

    public bool equipWeapon = false;
    bool invincible = false;
    bool alreadyAttack = false;

    public void EquipWeapon()
    {
        equipWeapon = true;
        if (Weapon != null)
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
            StartCoroutine(OnHitBox(predelay, duration));

            lastAttackTime = Time.time;
        }
    }

    IEnumerator OnHitBox(float predelay, float duration)
    {
        yield return new WaitForSeconds(predelay);
        hitBox.enabled = true;
        yield return new WaitForSeconds(duration);
        hitBox.enabled = false;
        alreadyAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == transform.gameObject)
            return;
        if (alreadyAttack)
            return;
        CharacterCombat enemyCombat = other.GetComponent<CharacterCombat>();
        if (enemyCombat != null)
        {
            if (!enemyCombat.invincible)
            {
                GiveDamage(enemyCombat);
                alreadyAttack = true;
            }
        }
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
        OnDie?.Invoke();
    }

    public void OnInvincible()
    {
        invincible = true;
    }

    public void OffInvincible()
    {
        invincible = false;
    }
}
