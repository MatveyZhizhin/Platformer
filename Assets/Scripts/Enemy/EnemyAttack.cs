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
    private EnemyMovement _trafficPermission;

    private void Update()
    {
        
    }

    private void Attack()
    {
        if(_attackDelay <= 0)
        {

            Collider2D[] objects = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange);
                foreach(Collider2D player in objects)
                {
                    if (player.TryGetComponent(out Health health))
                    {
                        _trafficPermission._trafficPermission = false;
                        health.TakeDamage(_damage);
                    }
                }
                _attackDelay = _startAttackDelay;
        }
        else
        {
            _attackDelay -= Time.deltaTime;
            _trafficPermission._trafficPermission = true;
        }
    }
}
