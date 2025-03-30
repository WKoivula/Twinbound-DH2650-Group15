using UnityEngine;

public class PlayerBController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) move = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) move = 1f;

        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
