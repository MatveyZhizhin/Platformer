using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private Transform _groundDetect;

    [SerializeField] private float _groundDistans;

    private bool _moveRight = true;


    void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
        RaycastHit2D groundinfo = Physics2D.Raycast(_groundDetect.position, Vector2.down, _groundDistans);

        if (groundinfo.collider == false)
        {
            if (_moveRight == true)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                _moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0 , 0);
                _moveRight = true;
            }
        }
    }
}
