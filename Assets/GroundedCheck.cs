using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public Animator animator;
    public Player player;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ground check script");
        animator.SetBool("isJumping", false);
    }
}
