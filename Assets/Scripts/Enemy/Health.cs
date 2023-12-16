using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHp;
    private void Start()
    {
        _hp = _maxHp;
    }

    public void TakeDamage(int damage)
    {
        _hp -= damage;

        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
