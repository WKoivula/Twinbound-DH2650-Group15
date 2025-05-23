using UnityEngine;

public class FollowTop : MonoBehaviour
{
    public Transform targetFill;
    public float offsetY = 0.05f;
    private bool isPaused = false;

    void Update()
    {
        float targetTopY = targetFill.position.y + (targetFill.localScale.y * 0.5f);
        transform.position = new Vector3(transform.position.x, targetTopY + offsetY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isPaused && other.CompareTag("Player"))
        {
            PauseGame();
        }
    }

void PauseGame()
{
    Time.timeScale = 0f;

    PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
    if (pauseMenu != null)
    {
        pauseMenu.PauseDirectly();
    }
    else
    {
        Debug.LogWarning("PauseMenu not found in the scene.");
    }

    isPaused = true;
}
}
