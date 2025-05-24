using UnityEngine;

public abstract class BaseLaserController : MonoBehaviour
{
    [Header("Laser Configuration")]
    public Transform emitter;
    public float maxDistance = 20f;
    public LayerMask hitMask;

    [Header("Laser Visual Effects")]
    public GameObject startVFX;
    public GameObject endVFX;

    protected LineRenderer lr;
    protected PlatformCharge lastPlatformHit = null;
    private bool hasPaused = false;

    protected virtual void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.startWidth = 0.5f;
            lr.endWidth = 0.5f;
            lr.enabled = false;
        }

        if (startVFX != null) startVFX.SetActive(false);
        if (endVFX != null) endVFX.SetActive(false);
    }

    protected void FireLaser(Vector3 origin, Vector3 direction)
    {
        if (lr == null) return;

        Vector3 endPoint = origin + direction * maxDistance;
        PlatformCharge currentHit = null;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, hitMask))
        {
            endPoint = hit.point;
            PlatformCharge platform = hit.collider.GetComponent<PlatformCharge>();
            if (platform != null)
            {
                currentHit = platform;
            }
           if (hit.collider.CompareTag("Player") && !hasPaused)
            {
                PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
                if (pauseMenu != null)
                {
                    pauseMenu.PauseDirectly();
                    hasPaused = true;
                }
            }
        }

        lr.SetPosition(0, origin);
        lr.SetPosition(1, endPoint);
        lr.enabled = true;

        if (startVFX != null)
        {
            startVFX.SetActive(true);
            startVFX.transform.position = origin;
        }

        if (endVFX != null)
        {
            endVFX.SetActive(true);
            endVFX.transform.position = endPoint;
        }

        HandlePlatformHit(currentHit, origin.x);
    }

    protected void HandlePlatformHit(PlatformCharge currentHit, float emitterX)
    {
        if (lastPlatformHit != currentHit)
        {
            if (lastPlatformHit != null)
                lastPlatformHit.SetLaserState(false, 0f);

            if (currentHit != null)
                currentHit.SetLaserState(true, emitterX);

            lastPlatformHit = currentHit;
        }
        else if (currentHit != null)
        {
            currentHit.SetLaserState(true, emitterX);
        }
    }

    protected void StopLaser()
    {
        if (lr != null) lr.enabled = false;
        if (startVFX != null) startVFX.SetActive(false);
        if (endVFX != null) endVFX.SetActive(false);

        if (lastPlatformHit != null)
        {
            lastPlatformHit.SetLaserState(false, 0f);
            lastPlatformHit = null;
        }
    }
}
