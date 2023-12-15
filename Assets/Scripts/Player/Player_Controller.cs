using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player_Controller : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float rayDistance;
        [SerializeField] private float jumpForce;
        [SerializeField] private float strongImpulse = 6000;
        [SerializeField] private LayerMask whatIsGround;

        private bool _lockDash = false;

        private Rigidbody2D playerRigidbody;

        private bool facingRight = true;
        private SpriteRenderer sr;

        private void Start()
        {
            TryGetComponent(out playerRigidbody);
            sr = GetComponent<SpriteRenderer>();
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
           
            if (!facingRight && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight && moveInput < 0)
            {
                Flip();
            }

            playerRigidbody.velocity = new Vector2(moveInput * speed, playerRigidbody.velocity.y);
        }

        private void Jump()
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, whatIsGround);

            if (hitInfo.collider != null)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }              
            }
        }  
        
        private void Flip()
        {
            facingRight = !facingRight;
            sr.flipX = !sr.flipX;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, Vector2.down * rayDistance);
        }

        private void Dash()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_lockDash)
            {
                _lockDash = true;
                Invoke("LockDash", 2f);
                
                playerRigidbody.velocity = new Vector2(0,0);

                if (playerRigidbody.transform.localScale.x < 0)
                {
                    playerRigidbody.AddForce(Vector2.left * strongImpulse);
                }
                else
                {
                    playerRigidbody.AddForce(Vector2.right * strongImpulse);
                }
            }
            
        }

        private void LockDash()
        {
            _lockDash = false;
        }
    }
}
