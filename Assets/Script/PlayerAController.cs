using UnityEngine;

public class PlayerAController : Player
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            GameManager.Instance.TriggerSwap();
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        Movement(KeyCode.A, KeyCode.D, KeyCode.UpArrow);
        base.Update();
    }
}
