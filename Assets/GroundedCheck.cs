using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    public Animator animator;
    public Player player;

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isJumping", false);
    }
}
