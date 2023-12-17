using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeAttack;
    [SerializeField] private float startTimeAttack;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask _whatIsPlayer;

    private Animator anim;

    private void Awake()
    {
        TryGetComponent(out anim);
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, ~_whatIsPlayer);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }
        }
    }

    private void Update()
    {
        if(timeAttack <= 0)
        {
            if(Input.GetMouseButton(0))
            {
                anim.SetTrigger("attack");               
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
