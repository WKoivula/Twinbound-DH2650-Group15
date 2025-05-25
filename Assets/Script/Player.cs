using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float moveSpeed = 2f;

    AudioSource jumpSound, footstepSound;
    public float jumpForce = 2f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 1f;
    public Transform groundCheck; 
    public float groundRadius = 0.2f;
    public float coyoteTime = 0.1f;
    public float bufferedJumpTime = 0.15f;

    public GameObject runningParticles;
    public Transform runningParticlePos;

    public float footstepCooldown = 0.4f;
    private float lastFootstepTime = -1f;

    private float fallMultiplier = 2.0f;

    protected Rigidbody rb;
    public Animator animator;
    public SpriteRenderer sprite;
    private bool wasGrounded = false;
    private bool inCoyoteTime = false;
    private SpriteRenderer spriteRenderer;
    private Material spriteMat;

    private MovingPlatform currentPlatform = null;
    private bool wasOnMovingPlatform = false;
    private bool isOnMovingPlatform = false;
    private bool justJumpedFromMovingPlatform = false;

    private float move = 0f;
    private bool bufferedAJump = false;

    private ParticleSystem runningParticleSystem;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpSound = GetComponents<AudioSource>()[0];
        footstepSound = GetComponents<AudioSource>()[1];
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteMat = spriteRenderer.material;
    }

    public void Movement(KeyCode left, KeyCode right, KeyCode jump)
    {
        move = 0f;
        if (Input.GetKey(left)) move = -1f;
        if (Input.GetKey(right)) move = 1f;

        Vector3 velocity = rb.linearVelocity;

        if (justJumpedFromMovingPlatform)
        {
            velocity += new Vector3(currentPlatform.Velocity.x, 0, 0);
            wasOnMovingPlatform = true;
            justJumpedFromMovingPlatform = false;
        }
        else if (wasOnMovingPlatform)          
        {
            if (Mathf.Abs(move) > 0f)
            {
                velocity.x = Mathf.Lerp(velocity.x, move * moveSpeed, 0.05f);
            }
        }
        else if (isOnMovingPlatform)
        {
            velocity.x = move * moveSpeed + currentPlatform.Velocity.x; // Make player sticks to moving platform
        }
        else
        {
            velocity.x = move * moveSpeed;
        }
        rb.linearVelocity = velocity;

        bool grounded = IsGrounded();

        if (!wasGrounded && grounded)
        {
            SpawnDustParticle();
        }

        if (wasGrounded && !grounded && rb.linearVelocity.y <= 0)
        {
            inCoyoteTime = true;
            StartCoroutine(CoyoteTime());
        }

        if (grounded && move != 0f  && Time.time - lastFootstepTime >= footstepCooldown)
        {
            PlayFootStep();
            SpawnDustParticle();
            lastFootstepTime = Time.time;
        }

        bool jumping = false;
        if (Input.GetKeyDown(jump))
        {
            jumping = true;
            bufferedAJump = true;
            StartCoroutine(BufferedJump());
        }

        if ((jumping || bufferedAJump) && (grounded || inCoyoteTime))
        {
            Jump();
        }

        if (Input.GetKeyUp(jump))
        {
            bufferedAJump = false;
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y / 2, rb.linearVelocity.z);
            }
        }

        if (rb.linearVelocity.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (wasGrounded != grounded)
        {
            animator.SetBool("isJumping", !grounded);
        }

        wasGrounded = grounded;
    }

    private void FixedUpdate()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(move));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    protected void Update()
    {

    }

    private void Jump()
    {
        jumpSound.Play();
        SpawnDustParticle();
        inCoyoteTime = false;

        Vector3 jumpVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);

        if (isOnMovingPlatform)
        {
            justJumpedFromMovingPlatform = true;
        }

        rb.linearVelocity = jumpVelocity;
        animator.SetBool("isJumping", true);
    }

    private void SpawnDustParticle()
    {
        GameObject particles = Instantiate(runningParticles, runningParticlePos.position, runningParticlePos.rotation);
        particles.transform.rotation = Quaternion.Euler(new Vector3(0f, transform.localScale.x == -1f ? 180f : 0f, 0f));
    }
    
    public bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundRadius, groundLayer);
        isOnMovingPlatform = false;
        currentPlatform = null;

        foreach (Collider collider in colliders)
        {
            MovingPlatform platform = collider.GetComponent<MovingPlatform>();
            if (platform != null && Mathf.Abs(platform.Velocity.x) > 0.1f)
            {
                isOnMovingPlatform = true;
                currentPlatform = platform;
            }

            if (!wasGrounded)
            {
                wasOnMovingPlatform = false;
            }

            return true;
        }

        return false;
    }

    IEnumerator BufferedJump()
    {
        yield return new WaitForSeconds(bufferedJumpTime);
        bufferedAJump = false;
    }

    IEnumerator CoyoteTime()
    {
        yield return new WaitForSeconds(coyoteTime);
        inCoyoteTime = false;
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
