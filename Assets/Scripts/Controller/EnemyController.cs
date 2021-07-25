using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public CharacterCombat combat;
    public EnemyAnimator enemyAnim;
    Transform target;

    public float detectionSize;
    float sqrDetectionSize;
    public float attackSize;
    float sqrAttackSize;

    public float stiffTime;
    public float attackTime;

    private void OnEnable()
    {
        combat.OnHitted += OnHitted;
        combat.OnDie += Die;
    }

    private void Start()
    {
        target = Player.Instance.transform;
        sqrDetectionSize = detectionSize * detectionSize;
        sqrAttackSize = attackSize * attackSize;
    }

    private void Update()
    {
        float sqrDistance = (target.position - transform.position).sqrMagnitude;
        if (sqrDistance < sqrDetectionSize)
        {
            enemyAnim.Walk();
            agent.SetDestination(target.position);
            if (sqrDistance < sqrAttackSize)
            {
                combat.Attack();
                agent.isStopped = true;
                StartCoroutine(Delay(attackTime));
            }
        }
        else
        {
            enemyAnim.Idle();
            agent.ResetPath();
        }   
    }

    void OnHitted()
    {
        agent.isStopped = true;
        StartCoroutine(Delay(stiffTime));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        agent.isStopped = false;
    }

    void Die()
    {
#if UNITY_EDITOR
        Debug.Log("Enemy is dead");
#endif
        StartCoroutine(ReturnObject());
    }

    IEnumerator ReturnObject()
    {
        this.enabled = false;
        yield return new WaitForSeconds(2f);
        this.enabled = true;
        PoolingManager.Instance.ReturnObject(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackSize);
    }
}
