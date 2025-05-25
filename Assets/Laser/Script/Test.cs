using UnityEngine;

public class DebugPositionLogger : MonoBehaviour
{
    public bool logContinuously = true;  
    public float logInterval = 1f;      

    private float timer = 0f;

    void Update()
    {
        if (logContinuously)
        {
            timer += Time.deltaTime;
            if (timer >= logInterval)
            {
                LogPosition();
                timer = 0f;
            }
        }
    }

    public void LogPosition()
    {
        Vector3 pos = transform.position;
        Debug.Log($"[{gameObject.name}] Position: x={pos.x:F2}, y={pos.y:F2}, z={pos.z:F2}");
    }
}
