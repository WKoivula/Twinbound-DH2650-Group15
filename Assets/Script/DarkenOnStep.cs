using UnityEngine;

public class DarkenOnStep : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D targetLight; 
    private bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered)
        {
            if (targetLight != null)
            {
                targetLight.intensity = 0f;
            }

            hasTriggered = true;
            Debug.Log("Light turned off!");
        }
    }
}
