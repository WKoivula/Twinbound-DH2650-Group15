using UnityEngine;

/// <summary>
/// Base class for laser controllers. Handles firing lasers, hit detection, visual effects (VFX),
/// and platform activation logic. Subclasses can use FireLaser to shoot and StopLaser to stop.
/// </summary>
public abstract class BaseLaserController : MonoBehaviour
{
    [Header("Laser Configuration")]
    public Transform emitter;               // The Transform of the laser emitter (typically the nozzle or origin point)
    public float maxDistance = 10f;         // Maximum range of the laser
    public LayerMask hitMask;               // Layer mask to filter raycast targets

    [Header("Laser Visual Effects")]
    public GameObject startVFX;             // VFX at the laser's origin
    public GameObject endVFX;               // VFX at the laser's endpoint

    // Internal state
    protected LineRenderer lr;              // LineRenderer used to draw the laser beam
    protected PlatformCharge lastPlatformHit = null; // Reference to the platform hit in the previous frame

    /// <summary>
    /// Initializes components, disables LineRenderer and visual effects.
    /// </summary>
    protected virtual void Start()
    {
        lr = GetComponent<LineRenderer>();
        if (lr != null)
        {
            lr.startWidth = 0.5f;           // Set laser width
            lr.endWidth = 0.5f;
            lr.enabled = false;             // Laser is hidden by default
        }

        if (startVFX != null) startVFX.SetActive(false);  // Hide start VFX
        if (endVFX != null) endVFX.SetActive(false);      // Hide end VFX
    }

    /// <summary>
    /// Fires a laser from the given origin in the specified direction. Updates VFX and detects hits.
    /// </summary>
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
            if (hit.collider.CompareTag("Player"))
            {
                PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
                if (pauseMenu != null)
                {
                    pauseMenu.PauseDirectly(); // pause on laser hit
                }
            }
        }

        // Draw the laser beam
        lr.SetPosition(0, origin);
        lr.SetPosition(1, endPoint);
        lr.enabled = true;

        // Show start VFX
        if (startVFX != null)
        {
            startVFX.SetActive(true);
            startVFX.transform.position = origin;
        }

        // Show end VFX
        if (endVFX != null)
        {
            endVFX.SetActive(true);
            endVFX.transform.position = endPoint;
        }

        // Handle platform activation
        HandlePlatformHit(currentHit, origin.x);
    }

    /// <summary>
    /// Handles platform laser state switching based on hit status.
    /// </summary>
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

    /// <summary>
    /// Stops the laser and resets states.
    /// </summary>
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
