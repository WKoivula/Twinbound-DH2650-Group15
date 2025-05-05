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

    void Update()
    {
        Movement(Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.D),Input.GetKeyDown(KeyCode.UpArrow));
    }
}
