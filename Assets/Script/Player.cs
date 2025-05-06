using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2f;

    AudioSource jumpSound, footstepSound;
    public float jumpForce = 2f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;
    private float footstepCooldown = 0.4f;
    private float lastFootstepTime = -1f;

    private float fallMultiplier = 2.0f;

    protected Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpSound = GetComponents<AudioSource>()[0];
        footstepSound = GetComponents<AudioSource>()[1];
    }

    public void Movement(KeyCode left, KeyCode right, KeyCode jump)
    {
        float move = 0f;
        if (Input.GetKey(left)) move = -1f;
        if (Input.GetKey(right)) move = 1f;
     


        Vector3 velocity = rb.linearVelocity;
        velocity.x = move * moveSpeed;
        rb.linearVelocity = velocity;

        if (IsGrounded() && move != 0f  && Time.time - lastFootstepTime >= footstepCooldown)
        {
            PlayFootStep();
            lastFootstepTime = Time.time;
        }

        if (Input.GetKeyDown(jump) && IsGrounded())
        {
            jumpSound.Play();
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }

        if (Input.GetKeyUp(jump) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y / 2, rb.linearVelocity.z);
        }
    }

    protected virtual void Update()
    {
        //if (rb.linearVelocity.y < 0)
        //{
        //    rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //}
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

    private void PlayFootStep()
    {
        footstepSound.volume = Random.Range(0.7f, 0.75f);
        footstepSound.pitch = Random.Range(0.8f, 1.2f);
        footstepSound.Play();
    }
    }
