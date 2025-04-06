using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public Collider2D gateCollider; // Assign the collider (e.g. BoxCollider2D) of the gate in Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        gateCollider.enabled = false; // Make gate passable
        Debug.Log("false"); // ← 这里要用 Debug.Log() 而不是 console()
    }

    void OnTriggerExit2D(Collider2D other)
    {
        gateCollider.enabled = true; // Make gate block again
        Debug.Log("true"); // ← 同上
    }
}
