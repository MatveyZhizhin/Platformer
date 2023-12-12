using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player_Controller : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float rayDistance;
        [SerializeField] private float jumpForce;

        [SerializeField] private LayerMask whatIsGround;

        private Rigidbody2D playerRigidbody;

        private bool facingRight = true;

        private void Start() => TryGetComponent(out playerRigidbody);

        private void Update()
        {
            Jump();
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
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, Vector2.down * rayDistance);
        }
    }
}
