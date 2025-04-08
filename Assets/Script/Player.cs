using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;

    protected Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Movement(bool left, bool right, bool jump)
    {
        float move = 0f;
        if (left) move = -1f;
        if (right) move = 1f;


        Vector3 velocity = rb.linearVelocity;
        velocity.x = move * moveSpeed;
        rb.linearVelocity = velocity;

        if (jump && IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Deadly"))
        {
            Destroy(gameObject);
        }
    }
}
