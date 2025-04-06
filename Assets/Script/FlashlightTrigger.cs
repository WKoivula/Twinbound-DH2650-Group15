using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightTrigger : MonoBehaviour
{
    public Light2D flashlight; 
    public float targetIntensity = 10f;  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (flashlight != null)
        {
            flashlight.intensity = targetIntensity; 
        }
    }
}
