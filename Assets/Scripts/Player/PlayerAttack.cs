using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float _timeAttack;
    [SerializeField] private float _startTimeAttack;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _layerToIgnore;

    private Animator anim;

    private void Awake()
    {
        TryGetComponent(out anim);
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, ~_layerToIgnore);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
        }
    }

    private void Update()
    {
        if(_timeAttack <= 0)
        {
            if(Input.GetMouseButton(0))
            {
                anim.SetTrigger("attack");               
                _timeAttack = _startTimeAttack;
            }
            
        }
        else
        {
            _timeAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}
