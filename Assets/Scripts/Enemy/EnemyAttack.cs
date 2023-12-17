using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _startAttackDelay;
    private float _attackDelay;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _enemy;
    private EnemyMovement _enemyMovement;


    private void Awake()
    {
        TryGetComponent(out _enemyMovement);
    }

    private void Update()
    {
        Attack();   
    }

    private void Attack()
    {
        if(_attackDelay <= 0)
        {

            Collider2D[] objects = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, ~_enemy);
                foreach(Collider2D player in objects)
                {
                    if (player.TryGetComponent(out Health health))
                    {
                        _enemyMovement._trafficPermission = false;
                        health.TakeDamage(_damage);
                    }
                }
                _attackDelay = _startAttackDelay;
        }
        else
        {
            _attackDelay -= Time.deltaTime;
            _enemyMovement._trafficPermission = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
