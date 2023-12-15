using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rayDistance;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _strongImpulse = 6000;
        [SerializeField] private LayerMask _whatIsGround;

        private bool _lockDash = false;

        private Rigidbody2D _playerRigidbody;

        private bool _facingRight = true;
        private SpriteRenderer _sr;

        private void Start()
        {
            TryGetComponent(out _playerRigidbody);
            _sr = GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            Jump();
            Dash();
            
        }

        private void FixedUpdate()
        {
            Move();           
        }

        private void Move()
        {
            var moveInput = Input.GetAxis("Horizontal");           
           
            if (!_facingRight && moveInput > 0)
            {
                Flip();
            }
            else if (_facingRight && moveInput < 0)
            {
                Flip();
            }

            _playerRigidbody.velocity = new Vector2(moveInput * _speed, _playerRigidbody.velocity.y);
        }

        private void Jump()
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, _whatIsGround);

            if (hitInfo.collider != null)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _playerRigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }              
            }
        }  
        
        private void Flip()
        {
            _facingRight = !_facingRight;
            _sr.flipX = !_sr.flipX;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, Vector2.down * _rayDistance);
        }

        private void Dash()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_lockDash)
            {
                _lockDash = true;
                Invoke("LockDash", 2f);
                
                _playerRigidbody.velocity = new Vector2(0,0);

                if (_sr.flipX == true)
                {
                    _playerRigidbody.AddForce(Vector2.left * _strongImpulse);
                }
                else
                {
                    _playerRigidbody.AddForce(Vector2.right * _strongImpulse);
                }
            }
            
        }

        private void LockDash()
        {
            _lockDash = false;
        }
    }
}
