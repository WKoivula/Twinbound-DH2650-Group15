using UnityEngine;

public class LaserHitScan : MonoBehaviour
{
    public Transform emitter;             
    public float maxDistance = 15f;      
    public LayerMask hitMask;            
    public Transform endVFX;            

    private LineRenderer lr;
    private PlatformCharge lastPlatformHit = null;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (emitter == null || lr == null) return;

        Vector3 origin = emitter.position;
        Vector3 direction = Vector3.down;
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
        }

      
        lr.SetPosition(0, origin);
        lr.SetPosition(1, endPoint);

        if (endVFX != null)
        {
            endVFX.position = endPoint;
        }

       
        if (lastPlatformHit != currentHit)
        {
            if (lastPlatformHit != null)
                lastPlatformHit.SetLaserState(false, 0f); 

            if (currentHit != null)
                currentHit.SetLaserState(true, emitter.position.x); 

            lastPlatformHit = currentHit;
        }
        else if (currentHit != null)
        {
            
            currentHit.SetLaserState(true, emitter.position.x);
        }
    }
}
