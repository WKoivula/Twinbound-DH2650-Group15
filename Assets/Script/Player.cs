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
    public Animator animator;
    public SpriteRenderer sprite;
    private bool wasGrounded = false;

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

        bool grounded = IsGrounded();

        if (grounded && move != 0f  && Time.time - lastFootstepTime >= footstepCooldown)
        {
            PlayFootStep();
            lastFootstepTime = Time.time;
        }

        if (Input.GetKeyDown(jump) && grounded)
        {
            jumpSound.Play();
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }

        if (Input.GetKeyUp(jump) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y / 2, rb.linearVelocity.z);
        }

        if (rb.linearVelocity.x > 0)
        {
            sprite.flipX = true;
        }
        else if (rb.linearVelocity.x < 0)
        {
            sprite.flipX = false;
        }

        if (wasGrounded != grounded)
        {
            animator.SetBool("isJumping", !grounded);
        }

        wasGrounded = grounded;
    }

    protected virtual void Update()
    {
        //if (rb.linearVelocity.y < 0)
        //{
        //    rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //}
    }

    private void FixedUpdate()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
    }

    private void PlayFootStep()
    {
        footstepSound.volume = Random.Range(0.7f, 0.75f);
        footstepSound.pitch = Random.Range(0.8f, 1.2f);
        footstepSound.Play();
    }
    }
