using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeAttack;
    [SerializeField] private float startTimeAttack;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask enemy;
    [SerializeField] private float attackRange;
    [SerializeField] private Animator anim;
    
    private void Update()
    {
        if(timeAttack <= 0)
        {
            if(Input.GetMouseButton(0))
            {
                anim.SetTrigger("attack");
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemy);
                foreach(Collider2D enemy in enemies)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
                timeAttack = startTimeAttack;
            }
            
        }
        else
        {
            timeAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
