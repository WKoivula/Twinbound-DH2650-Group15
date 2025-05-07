using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    private Animator animator;
    public Player player;

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isJumping", !player.IsGrounded());
    }
}
